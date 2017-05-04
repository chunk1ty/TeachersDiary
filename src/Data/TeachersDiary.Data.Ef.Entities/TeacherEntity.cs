using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersDiary.Data.Ef.Entities
{
    [Table("Teachers")]
    public class TeacherEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid ShoolId { get; set; }

        public virtual SchoolEntity School { get; set; }
    }
}
