using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersDiary.Data.Domain
{
    public class SchoolDomain
    {
        public SchoolDomain()
        {
            Teachers = new HashSet<TeacherDomain>();
            Classes = new HashSet<ClassDomain>();
        }

        public string Name { get; set; }

        public IEnumerable<TeacherDomain> Teachers { get; set; }

        public IEnumerable<ClassDomain> Classes { get; set; }
    }
}
