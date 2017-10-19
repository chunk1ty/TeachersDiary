using System;
using System.Collections.Generic;
using System.Linq;

using Bytes2you.Validation;

using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts;

namespace TeachersDiary.Data.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptingService _encryptingService;
        private readonly IEntityFrameworkGenericRepository<AbsenceEntity> _entityFrameworkGenericRepository;

        public AbsenceService(
            IEncryptingService encryptingService, 
            IUnitOfWork unitOfWork, 
            IEntityFrameworkGenericRepository<AbsenceEntity> entityFrameworkGenericRepository)
        {
            _encryptingService = encryptingService;
            _unitOfWork = unitOfWork;
            _entityFrameworkGenericRepository = entityFrameworkGenericRepository;
        }

        public void CalculateStudentAbsences(List<StudentDomain> students, string month)
        {
            Guard.WhenArgument(students, nameof(students)).IsNull().Throw();
            Guard.WhenArgument(month, nameof(month)).IsNull().Throw();

            var selectedMonthId = int.Parse(month);

            // calculate absences for the first time for selected month
            if (!students.FirstOrDefault().Absences.Any(x => x.MonthId == selectedMonthId))
            {
                AddAbsences(students, selectedMonthId);
            }
            else
            {
                UpdateAbsebces(students, selectedMonthId);
            }

            _unitOfWork.Commit();
        }

        private void AddAbsences(List<StudentDomain> students, int selectedMonthId)
        {
            var absences = new List<AbsenceEntity>();

            foreach (var student in students)
            {
                var studentAbsences = student.Absences.Where(x => x.MonthId < selectedMonthId).ToList();

                var totalExcusedAbsences = studentAbsences.Sum(x => x.Excused);
                var totalNotExcusedAbsences = studentAbsences.Sum(x => x.NotExcused);

                var studentId = _encryptingService.DecodeId(student.Id);

                var absence = new AbsenceEntity()
                {
                    Excused = student.EnteredTotalExcusedAbsences - totalExcusedAbsences,
                    NotExcused = student.EnteredTotalNotExcusedAbsences - totalNotExcusedAbsences,
                    StudentId = studentId,
                    MonthId = selectedMonthId
                };

                absences.Add(absence);
            }

            _entityFrameworkGenericRepository.AddRange(absences);
        }

        private void UpdateAbsebces(List<StudentDomain> students, int selectedMonthId)
        {
            foreach (var student in students)
            {
                var studentAbsences = student.Absences.Where(x => x.MonthId < selectedMonthId).ToList();

                var totalExcusedAbsences = studentAbsences.Sum(x => x.Excused);
                var totalNotExcusedAbsences = studentAbsences.Sum(x => x.NotExcused);

                var studentId = _encryptingService.DecodeId(student.Id);
                var studentAbsence = student.Absences.LastOrDefault();

                if (studentAbsence != null)
                {
                    var absenseId = _encryptingService.DecodeId(studentAbsence.Id);

                    var absence = new AbsenceEntity()
                    {
                        Id = absenseId,
                        Excused = student.EnteredTotalExcusedAbsences - totalExcusedAbsences,
                        NotExcused = student.EnteredTotalNotExcusedAbsences - totalNotExcusedAbsences,
                        StudentId = studentId,
                        MonthId = selectedMonthId
                    };

                    _entityFrameworkGenericRepository.Update(absence);
                }
                //if user do not fill row when update record
                else
                {
                    var absence = new AbsenceEntity()
                    {
                        Excused = 0,
                        NotExcused = 0,
                        StudentId = studentId,
                        MonthId = selectedMonthId
                    };

                    _entityFrameworkGenericRepository.Add(absence);
                }
            }
        }
    }
}