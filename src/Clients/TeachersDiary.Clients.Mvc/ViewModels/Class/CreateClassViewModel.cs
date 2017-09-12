using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.ViewModels.Class
{
    public class CreateClassViewModel : IMap<ClassDomain>
    {
        [Display(Name = "Име")]
        [Required(ErrorMessage = "Името на класът е задължително")]
        public string Name { get; set; }

        public string ClassTeacherId { get; set; }

        public SelectList Teachers { get; set; }
    }
}