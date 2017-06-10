using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersDiary.Data.Entities
{
    [Table("Students")]
    public class StudentEntity
    {
        public StudentEntity()
        {
            Absences = new List<AbsenceEntity>();
        }

        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int Number { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ClassId { get; set; }
        public virtual ClassEntity Class { get; set; }

        public IList<AbsenceEntity> Absences { get; set; }
    }
}
