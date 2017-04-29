using System;
using System.Collections.Generic;

namespace TeacherDiary.Data.Entities
{
    public class School
    {
        public School()
        {
            Teachers = new HashSet<Teacher>();
            Classes = new HashSet<Class>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Teacher> Teachers { get; set; }

        public IEnumerable<Class> Classes { get; set; }
    }
}
