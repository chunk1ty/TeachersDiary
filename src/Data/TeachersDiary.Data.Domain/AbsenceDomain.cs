using System;

using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Data.Domain
{
   public class AbsenceDomain : IMapTo<AbsenceEntity>, IMapFrom<AbsenceEntity>
    {
        public int Id { get; set; }

        public Guid StudentId { get; set; }

        public int MonthId { get; set; }

        public double Excused { get; set; }

        public double NotExcused { get; set; }
    }
}
