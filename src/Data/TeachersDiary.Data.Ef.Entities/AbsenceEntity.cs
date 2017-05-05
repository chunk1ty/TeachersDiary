using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersDiary.Data.Ef.Entities
{
    [Table("Absences")]
    public class AbsenceEntity
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public virtual StudentEntity Student { get; set; }

        public int MonthId { get; set; }

        public double Excused { get; set; }

        public double NotExcused { get; set; }
    }
}
