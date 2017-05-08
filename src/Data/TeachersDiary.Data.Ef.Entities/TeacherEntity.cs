using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersDiary.Data.Ef.Entities
{
    [Table("Teachers")]
    public class TeacherEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? SchoolId { get; set; }
        public virtual SchoolEntity School { get; set; }
    }
}
