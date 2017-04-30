using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeacherDiary.Clients.Mvc.Infrastructure.Mapping.Contracts;
using TeacherDiary.Clients.Mvc.ViewModels.Student;
using TeacherDiary.Common.Constants.Validation;

namespace TeacherDiary.Clients.Mvc.ViewModels.Class
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