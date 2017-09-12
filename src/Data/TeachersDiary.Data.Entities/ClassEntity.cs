using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TeachersDiary.Common.Constants;

namespace TeachersDiary.Data.Entities
{
    [Table("Classes")]
    public class ClassEntity
    {
        public ClassEntity()
        {
            Students = new List<StudentEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SchoolId { get; set; }
        public virtual SchoolEntity School { get; set; }

        public IList<StudentEntity> Students { get; set; }

        [Required]
        public string ClassTeacherId { get; set; }
    }
}
