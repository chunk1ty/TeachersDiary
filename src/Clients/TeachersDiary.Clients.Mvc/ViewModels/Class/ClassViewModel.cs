using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeachersDiary.Clients.Mvc.ViewModels.Student;
using TeachersDiary.Common.Constants.Validation;
using TeachersDiary.Data.Domain;
using TeachersDiary.Data.Ef.Entities;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.ViewModels.Class
{
    public class ClassViewModel : IMapTo<ClassDomain>, IMapFrom<ClassDomain>
    {
        public ClassViewModel()
        {
            Students = new List<StudentViewModel>();
        }

        public Guid Id { get; set; }

        [MinLength(DbEntitesValidationConstants.ClassNameMinLength)]
        [MaxLength(DbEntitesValidationConstants.ClassNameMaxLength)]
        public string Name { get; set; }

        public List<StudentViewModel> Students { get; set; }
    }
}