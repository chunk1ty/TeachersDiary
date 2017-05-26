using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersDiary.Data.Entities
{
    [Table("SchoolAdmins")]
    public class SchoolAdminEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int SchoolId { get; set; }
        public virtual SchoolEntity School { get; set; }
    }
}
