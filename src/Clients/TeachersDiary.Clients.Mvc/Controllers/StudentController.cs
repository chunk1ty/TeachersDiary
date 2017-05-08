using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

using System.Web.Mvc.Expressions;

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
                var tokens = student.TotalNotExcusedAbsencesAsFractionNumber.Split(' ');

                if (tokens.Length == 1)
                {
                    if (tokens[0].IsContainsOnlyDigits())
                    {
                       // student.TotalNotExcusedAbsence = int.Parse(tokens[0]);
                        continue;
                    }

                    var errorMsg = string.Format("Некоректно въведени данни за {0}! След като въведете цялата част оставете интервал след което въведете и дробната", student.FirstName + " " + student.MiddleName + " " + student.LastName);

                    ModelState.AddModelError("", errorMsg);
                    return View("Index", model);
                }

                if (tokens.Length != 2)
                {
                    var errorMsg = string.Format("Некоректно въведени данни за {0}! След като въведете цялата част оставете интервал след което въведете и дробната", student.FirstName + " " + student.MiddleName + " " + student.LastName);

                    ModelState.AddModelError("", errorMsg);
                    return View("Index", model);
                }

                var intPart = int.Parse(tokens[0]);
                var floatingPart = 0.0;

                if (tokens[1] == "1/3")
                {
                    floatingPart = 1.0 / 3;
                }
                else if (tokens[1] == "2/3")
                {
                    floatingPart = 2.0 / 3;
                }
                else
                {
                    var errorMsg = string.Format("Некоректно въведени данни за {0}! След като въведете цялата част оставете интервал след което въведете и дробната", student.FirstName + " " + student.MiddleName + " " + student.LastName);

                    ModelState.AddModelError("", errorMsg);
                    return View("Index", model);
                }

                //student.TotalNotExcusedAbsences = intPart + floatingPart;
            }

            var studentDomains = _mappingService.Map<List<StudentDomain>>(model.Students);

            _absenceService.CalculateStudentsAbsencesForLastMonth(studentDomains);

            return this.RedirectToAction<StudentController>(x => x.Index(model.EncodedId));
        }
    }
}