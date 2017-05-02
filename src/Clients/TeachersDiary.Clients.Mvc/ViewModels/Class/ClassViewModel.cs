using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeachersDiary.Clients.Mvc.Infrastructure.Mapping.Contracts;
using TeachersDiary.Clients.Mvc.ViewModels.Student;
using TeachersDiary.Common.Constants.Validation;

namespace TeachersDiary.Clients.Mvc.ViewModels.Class
{
    public class ClassViewModel : IMapTo<Data.Entities.Class>, IMapFrom<Data.Entities.Class>
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