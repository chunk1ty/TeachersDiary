using System;
using TeachersDiary.Data.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.ViewModels.Absence
{
    public class AbsenceViewModel : IMapFrom<AbsenceDomain>, IMapTo<AbsenceDomain>
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int MonthId { get; set; }

        public double Excused { get; set; }

        public double NotExcused { get; set; }
    }
}