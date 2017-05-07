using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace TeachersDiary.Clients.Mvc.ViewModels.Account
{
    [ExcludeFromCodeCoverage]
    public class RegisterViewModel
    {        
        [Required(ErrorMessage = "Имeйла е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [StringLength(100, ErrorMessage = "Паролата трябва да е с дължина минимум {2} символа.", MinimumLength = 6)]
        [DataType(DataType.Password)]        
        public string Password { get; set; }

        [DataType(DataType.Password)]       
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Паролата несъвпада.")]
        public string ConfirmPassword { get; set; }
      
        public string SelectedSchool { get; set; }
        public IEnumerable<SelectListItem> Schools { get; set; }
    }
}