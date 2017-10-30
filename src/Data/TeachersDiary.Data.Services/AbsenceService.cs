using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Bytes2you.Validation;
using TeachersDiary.Common;
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
        private readonly IEntityFrameworkGenericRepository<AbsenceEntity> _absenceRepository;
        private readonly IQuerySettings<AbsenceEntity> _querySettings;

        public AbsenceService(
            IEncryptingService encryptingService, 
            IUnitOfWork unitOfWork, 
            IEntityFrameworkGenericRepository<AbsenceEntity> absenceRepository, IQuerySettings<AbsenceEntity> querySettings)
        {
            _encryptingService = encryptingService;
            _unitOfWork = unitOfWork;
            _absenceRepository = absenceRepository;
            _querySettings = querySettings;
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

        public async Task<OperationStatus> DeleteByClassAsyncId(string classId)
        {
            try
            {
                var decodedClassId = _encryptingService.DecodeId(classId);

                _querySettings.Include(x => x.Student);
                _querySettings.Where(x => x.Student.ClassId == decodedClassId);

                var absences = await _absenceRepository.GetAllAsync(_querySettings);

                if (!absences.Any())
                {
                    return new SuccessStatus();
                }

                _absenceRepository.DeleteRange(absences);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return new FailureStatus("Възникна грешка при изтриването на отсъствията. Моля свържете се със ситемният администратор.");
            }

            return new SuccessStatus();
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

            _absenceRepository.AddRange(absences);
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

                    _absenceRepository.Update(absence);
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

                    _absenceRepository.Add(absence);
                }
            }
        }
    }
}