using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TeachersDiary.Clients.Mvc.ViewModels.Account
{
    [ExcludeFromCodeCoverage]
    public class ForgotPasswordViewModel
    {        
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Имeйла е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл формат.")]
        public string Email { get; set; }
    }
}