using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;
using TeachersDiary.Data.Ef;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts;

namespace TeachersDiary.Data.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly ITeachersDiaryDbContext _teacherDiaryDbContext;
        private readonly IEncryptingService _encryptingService;


        public AbsenceService(ITeachersDiaryDbContext teacherDiaryDbContext, IEncryptingService encryptingService)
        {
            _teacherDiaryDbContext = teacherDiaryDbContext;
            _encryptingService = encryptingService;
        }

        public void CalculateStudentsAbsencesForLastMonth(List<StudentDomain> students)
        {
            Guard.WhenArgument(students, nameof(students)).IsNull().Throw();

            var absences = new List<AbsenceEntity>();

            if (students.FirstOrDefault().Absences.Count == 3)
            {
                foreach (var student in students)
                {
                    var totalExcusedAbsences = student.Absences.Sum(x => x.Excused);
                    var totalNotExcusedAbsences = student.Absences.Sum(x => x.NotExcused);
                    var studentId = _encryptingService.DecodeId(student.EncodedId);

                    var absence = new AbsenceEntity()
                    {
                        Excused = student.TotalExcusedAbsences - totalExcusedAbsences,
                        NotExcused = student.TotalNotExcusedAbsences - totalNotExcusedAbsences,
                        StudentId = studentId,
                        // MonthId = 4 == April
                        MonthId = 4
                    };

                    absences.Add(absence);
                }

                _teacherDiaryDbContext.Insert(absences);
            }
            else
            {
                foreach (var student in students)
                {
                    var totalExcusedAbsences = student.Absences.Take(3).Sum(x => x.Excused);
                    var totalNotExcusedAbsences = student.Absences.Take(3).Sum(x => x.NotExcused);
                    var studentId = _encryptingService.DecodeId(student.EncodedId);
                    var absenseId = _encryptingService.DecodeId(student.Absences.LastOrDefault().EncodedId);

                    var absence = new AbsenceEntity()
                    {
                        Id = absenseId,
                        Excused = student.TotalExcusedAbsences - totalExcusedAbsences,
                        NotExcused = student.TotalNotExcusedAbsences - totalNotExcusedAbsences,
                        StudentId = studentId,
                        // MonthId = 4 == April
                        MonthId = 4
                    };

                    absences.Add(absence);
                }

                _teacherDiaryDbContext.Update(absences);
            }

            _teacherDiaryDbContext.SaveChanges();
        }
    }
}