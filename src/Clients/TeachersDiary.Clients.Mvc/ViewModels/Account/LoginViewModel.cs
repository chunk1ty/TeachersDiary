using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TeachersDiary.Clients.Mvc.ViewModels.Account
{
    [ExcludeFromCodeCoverage]
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
        [Required(ErrorMessage = "Имeйла е задължителен.")]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Паролата е задължителен.")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
