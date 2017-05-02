using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TeachersDiary.Data.Ef.Entities
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

        public double TotalExcusedAbsences
        {
            get
            {
                return Absences.Sum(x => x.Excused);
            }
        }

        public double TotalNotExcusedAbsence
        {
            get
            {
                return Absences.Sum(x => x.NotExcused);
            }
        }

        public string TotalNotExcusedAbsenceAsString
        {
            get
            {
                var integerPart = (int)TotalNotExcusedAbsence;
                var floatingPart = TotalNotExcusedAbsence - Math.Truncate(TotalNotExcusedAbsence);
                var floatingPartAstring = floatingPart.ToString();
                var fractionalPart = string.Empty;

                if (floatingPartAstring.Contains(".3333"))
                {
                    fractionalPart = " 1/3";
                }

                if (floatingPartAstring.Contains(".6666"))
                {
                    fractionalPart = " 2/3";
                }

                var result = integerPart + fractionalPart;

                return result;
            }

        }
    }
}
