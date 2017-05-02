using System;

namespace TeachersDiary.Data.Entities
{
    public class Absence
    {
        public int Id { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int MonthId { get; set; }

        public double Excused { get; set; }

        public double NotExcused { get; set; }
    }
}
