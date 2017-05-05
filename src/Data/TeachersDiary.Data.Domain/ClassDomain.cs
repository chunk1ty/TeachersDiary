using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Data.Domain
{
    public class ClassDomain : IMapTo<ClassEntity>, IMapFrom<ClassEntity>
    {
        public ClassDomain()
        {
            Students = new HashSet<StudentDomain>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<StudentDomain> Students { get; set; }
    }
}
