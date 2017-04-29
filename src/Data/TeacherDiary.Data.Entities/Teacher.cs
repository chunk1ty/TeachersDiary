using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeacherDiary.Data.Entities
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
