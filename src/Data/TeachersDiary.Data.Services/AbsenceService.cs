using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;
using TeachersDiary.Data.Ef;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Data.Services.Contracts;

namespace TeachersDiary.Data.Services
{
    public class AbsenceService : IAbsenceService
    {
        private readonly ITeachersDiaryDbContext _teacherDiaryDbContext;

        public AbsenceService(ITeachersDiaryDbContext teacherDiaryDbContext)
        {
            _teacherDiaryDbContext = teacherDiaryDbContext;
        }

        public void CalculateStudentsAbsencesForLastMonth(List<StudentDto> students)
        {
            Guard.WhenArgument(students, nameof(students)).IsNull().Throw();

            var absences = new List<Absence>();

            if (students.FirstOrDefault().Absences.Count == 3)
            {
                foreach (var student in students)
                {
                    var totalExcusedAbsences = student.Absences.Sum(x => x.Excused);
                    var totalNotExcusedAbsences = student.Absences.Sum(x => x.NotExcused);

                    var absence = new Absence()
                    {
                        Excused = student.TotalExcusedAbsences - totalExcusedAbsences,
                        NotExcused = student.TotalNotExcusedAbsence - totalNotExcusedAbsences,
                        StudentId = student.Id,
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

                    var absence = new Absence()
                    {
                        Id = student.Absences.LastOrDefault().Id,
                        Excused = student.TotalExcusedAbsences - totalExcusedAbsences,
                        NotExcused = student.TotalNotExcusedAbsence - totalNotExcusedAbsences,
                        StudentId = student.Id,
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