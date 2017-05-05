using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Data.Domain;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class ClassController : BaseController
    {
        private readonly IClassService _classService;
        private readonly IAbsenceService _absenceService;
        private readonly IMappingService _mappingService;

        public ClassController(
            IClassService classService, 
            IAbsenceService absenceService, 
            IMappingService mappingService)
        {
            _classService = classService;
            _absenceService = absenceService;
            _mappingService = mappingService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var classDomains =  await _classService.GetAllAsync();

            var classViewModels = _mappingService.Map<IEnumerable<ClassViewModel>>(classDomains);

            return View(classViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> Students(Guid classId)
        {
            var classDomain = await _classService.GetClassWithStudentsByClassIdAsync(classId);

            var classViewModel = _mappingService.Map<ClassViewModel>(classDomain);

            return View(classViewModel);
        }

        [HttpPost]
        public ActionResult Students(ClassViewModel model)
        {
            foreach (var student in model.Students)
            {
                var tokens = student.TotalNotExcusedAbsenceAsString.Split(' ');

                if (tokens.Length == 1)
                {
                    if (IsDigitsOnly(tokens[0]))
                    {
                        student.TotalNotExcusedAbsence = int.Parse(tokens[0]);
                        continue;
                    }

                    var errorMsg = string.Format("Некоректно въведени данни за {0}! След като въведете цялата част оставете интервал след което въведете и дробната", student.FirstName + " " + student.MiddleName + " " + student.LastName);

                    ModelState.AddModelError("", errorMsg);
                    return View(model);
                }

                if (tokens.Length != 2)
                {
                    var errorMsg = string.Format("Некоректно въведени данни за {0}! След като въведете цялата част оставете интервал след което въведете и дробната", student.FirstName + " " + student.MiddleName + " " + student.LastName);

                    ModelState.AddModelError("", errorMsg);
                    return View(model);
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
                    return View(model);
                }

                student.TotalNotExcusedAbsence = intPart + floatingPart;
            }

            var studentDomains = _mappingService.Map<List<StudentDomain>>(model.Students); 

            _absenceService.CalculateStudentsAbsencesForLastMonth(studentDomains);

            return this.RedirectToAction<ClassController>(x => x.Students(model.Id));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ClassViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return View(model);
            }

            var classDomain = _mappingService.Map<ClassDomain>(model);

            _classService.Add(classDomain);

            return this.RedirectToAction<ClassController>(x => x.Index());
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid classId)
        {
            await _classService.DeleteById(classId);

            return this.RedirectToAction<ClassController>(x => x.Index());
        }

        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}