﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeachersDiary.Data.Entities
{
    [Table("Students")]
    public class StudentEntity
    {
        public StudentEntity()
        {
            Absences = new List<AbsenceEntity>();
        }

        [Key]
        public int Id { get; set; }

        [Index("IX_UniqueNumberInClass", 1, IsUnique = true)]
        public int Number { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        [Index("IX_UniqueNumberInClass", 2, IsUnique = true)]
        public int ClassId { get; set; }
        public virtual ClassEntity Class { get; set; }

        public IList<AbsenceEntity> Absences { get; set; }
    }
}
