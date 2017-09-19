using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersDiary.Data.Entities
{
    [Table("Schools")]
    public class SchoolEntity
    {
        public SchoolEntity()
        {
            Users = new HashSet<UserEntity>();
            Classes = new List<ClassEntity>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserEntity> Users { get; set; }

        public IList<ClassEntity> Classes { get; set; }
    }
}
