using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

using System.Web.Mvc.Expressions;
using Resources;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Common.Constants;
using TeachersDiary.Common.Extensions;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    [Authorize(Roles = ApplicationRole.Teacher)]
    public class StudentController : BaseController
    {
        private readonly IClassService _classService;
        private readonly IMappingService _mappingService;
        private readonly IAbsenceService _absenceService;

        public StudentController(IClassService classService, IMappingService mappingService, IAbsenceService absenceService)
        {
            _classService = classService;
            _mappingService = mappingService;
            _absenceService = absenceService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string classId)
        {
            var classDomain = await _classService.GetClassWithStudentsByClassIdAsync(classId);

            var classViewModel = _mappingService.Map<ClassViewModel>(classDomain);

            return View(classViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateAbsenses(ClassViewModel model)
        {
            foreach (var student in model.Students)
            {
                double totalNoExcusedAbsences;

                try
                {
                    totalNoExcusedAbsences = student.TotalNotExcusedAbsencesAsFractionNumber.FractionToDoubleNumber();
                }
                catch (Exception ex)
                {
                    var errorMsg =
                        $" {GlobalResources.IncorrectlyEnteredData + student.FirstName + " " + student.MiddleName + " " + student.LastName}! + { GlobalResources.FragmentErrorMessage }";

                    ModelState.AddModelError(string.Empty, errorMsg);

                    return View("Index", model);
                }

                student.TotalNotExcusedAbsences = totalNoExcusedAbsences;
            }

            var studentDomains = _mappingService.Map<List<StudentDomain>>(model.Students);

            _absenceService.CalculateStudentsAbsencesForLastMonth(studentDomains);

            return this.RedirectToAction<StudentController>(x => x.Index(model.EncodedId));
        }
    }
}