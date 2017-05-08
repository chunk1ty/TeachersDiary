using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeachersDiary.Clients.Mvc.ViewModels.Student;
using TeachersDiary.Common.Constants.Validation;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.ViewModels.Class
{
    public class ClassViewModel : IMapTo<ClassDomain>, IMapFrom<ClassDomain>
    {
        public ClassViewModel()
        {
            Students = new List<StudentViewModel>();
        }

        public string EncodedId { get; set; }

        [MinLength(DbEntitesValidationConstants.ClassNameMinLength)]
        [MaxLength(DbEntitesValidationConstants.ClassNameMaxLength)]
        public string Name { get; set; }

        public double TotalExcusedAbsences { get; set; }

        public string TotalNotExcusedAbsencesAsFractionNumber { get; set; }

        public List<StudentViewModel> Students { get; set; }
    }
}