using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachersDiary.Common.Constants.Validation;

namespace TeachersDiary.Data.Ef.Entities
{
    public class Class
    {
        public Class()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MinLength(DbEntitesValidationConstants.ClassNameMinLength)]
        [MaxLength(DbEntitesValidationConstants.ClassNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
