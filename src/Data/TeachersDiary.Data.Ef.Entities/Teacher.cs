using System;

namespace TeachersDiary.Data.Ef.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid ShoolId { get; set; }

        public virtual School School { get; set; }
    }
}
