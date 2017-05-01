using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using AutoMapper;
using TeacherDiary.Clients.Mvc.Infrastructure.Mapping;
using TeacherDiary.Clients.Mvc.ViewModels.Class;
using TeacherDiary.Data.Contracts;
using TeacherDiary.Data.Entities;
using TeacherDiary.Data.Services.Contracts;

namespace TeacherDiary.Clients.Mvc.Controllers
{
    public class ClassController : BaseController
    {
        private readonly IClassService _classService;
        private readonly IAbsenceService _absenceService;

        public ClassController(IClassService classService, IAbsenceService absenceService)
        {
            _classService = classService;
            _absenceService = absenceService;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var classesAsDbEntities =  await _classService.GetAllAsync();

            var classesAsViewModel = classesAsDbEntities.To<ClassViewModel>().ToList();

            return View(classesAsViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Students(Guid classId)
        {
            var classAsDbEntities = await _classService.GetClassWithStudentsByClassIdAsync(classId);

            var classAsViewModel = Mapper.Map<ClassViewModel>(classAsDbEntities);

            return View(classAsViewModel);
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

            var studentsAsDbEntities = model.Students.To<StudentDto>().ToList();

            _absenceService.CalculateStudentsAbsencesForLastMonth(studentsAsDbEntities);

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

            var classAsDbEntity = Mapper.Map<Class>(model);

            _classService.Add(classAsDbEntity);

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