using System.ComponentModel.DataAnnotations;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.ViewModels.Student
{
    public class CreateStudentViewModel : IMap<StudentDomain>
    {
        public string Id { get; set; }

        [Display(Name = "№")]
        [Required(ErrorMessage = "Номерът е задължителен")]
        [Range(1,100, ErrorMessage = "Номерът трябва да е в интервала от 1 до 100")]
        public int Number { get; set; }

        [Display(Name = "Име")]
        [Required(ErrorMessage = "Името е задължително")]
        public string FirstName { get; set; }

        [Display(Name = "Презиме")]
        [Required(ErrorMessage = "Презимето е задължителното")]
        public string MiddleName { get; set; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилията е задължителна")]
        public string LastName { get; set; }

        public string ClassId { get; set; }
    }
}