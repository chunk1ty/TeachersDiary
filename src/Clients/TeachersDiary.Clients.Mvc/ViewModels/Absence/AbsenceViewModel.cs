using System;
using TeachersDiary.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.ViewModels.Absence
{
    public class AbsenceViewModel : IMapFrom<AbsenceDomain>, IMapTo<AbsenceDomain>
    {
        public string EncodedId { get; set; }

        public string EncodedStudentId { get; set; }

        public int MonthId { get; set; }

        public double Excused { get; set; }

        public double NotExcused { get; set; }
    }
}