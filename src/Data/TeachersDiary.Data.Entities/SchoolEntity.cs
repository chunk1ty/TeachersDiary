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
            Teachers = new HashSet<TeacherEntity>();
            Classes = new HashSet<ClassEntity>();
            SchoolAdmins = new HashSet<SchoolAdminEntity>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TeacherEntity> Teachers { get; set; }

        public IEnumerable<ClassEntity> Classes { get; set; }

        public IEnumerable<SchoolAdminEntity> SchoolAdmins { get; set; }
    }
}
