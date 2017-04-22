using System.ComponentModel.DataAnnotations;
using TeacherDiary.Clients.Mvc.Infrastructure.Mapping.Contracts;
using TeacherDiary.Common.Constants.Validation;

namespace TeacherDiary.Clients.Mvc.ViewModels.Class
{
    public class ClassViewModel : IMapTo<Data.Entities.Class>, IMapFrom<Data.Entities.Class>
    {
        [MinLength(DbEntitesValidationConstants.ClassNameMinLength)]
        [MaxLength(DbEntitesValidationConstants.ClassNameMaxLength)]
        public string Name { get; set; }
    }
}