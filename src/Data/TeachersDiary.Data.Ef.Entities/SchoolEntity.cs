using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersDiary.Data.Ef.Entities
{
    [Table("Schools")]
    public class SchoolEntity
    {
        public SchoolEntity()
        {
            Teachers = new HashSet<TeacherEntity>();
            Classes = new HashSet<ClassEntity>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<TeacherEntity> Teachers { get; set; }

        public IEnumerable<ClassEntity> Classes { get; set; }
    }
}
