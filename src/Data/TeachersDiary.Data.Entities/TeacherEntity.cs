using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersDiary.Data.Entities
{
    [Table("Teachers")]
    public class TeacherEntity
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int SchoolId { get; set; }
        public virtual SchoolEntity School { get; set; }
    }
}
