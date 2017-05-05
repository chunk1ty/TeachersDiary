﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TeachersDiary.Data.Ef.Entities
{
    [Table("Students")]
    public class StudentEntity
    {
        public StudentEntity()
        {
            Absences = new HashSet<AbsenceEntity>();
        }

        [Key]
        public int Id { get; set; }

        public int Number { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ClassId { get; set; }
        public virtual ClassEntity Class { get; set; }

        public ICollection<AbsenceEntity> Absences { get; set; }
    }
}
