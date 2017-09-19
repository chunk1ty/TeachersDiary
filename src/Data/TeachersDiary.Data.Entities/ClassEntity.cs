using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        
        public string Name { get; set; }
        
        public int SchoolId { get; set; }
        public virtual SchoolEntity School { get; set; }

        public IList<StudentEntity> Students { get; set; }
        
        [Index(IsUnique = true)]
        [StringLength(36)]
        public string ClassTeacherId { get; set; }
    }
}
