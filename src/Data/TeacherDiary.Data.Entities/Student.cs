using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeacherDiary.Data.Entities
{
    public class Student
    {
        public Student()
        {
            Absences = new HashSet<Absence>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int Number { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Guid ClassId { get; set; }
        public virtual Class Class { get; set; }

        public ICollection<Absence> Absences { get; set; }
    }
}
