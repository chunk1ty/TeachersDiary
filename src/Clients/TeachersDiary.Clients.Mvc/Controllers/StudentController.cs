using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

using System.Web.Mvc.Expressions;
using Microsoft.AspNet.Identity;
using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Common.Extensions;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Mapping.Contracts;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class StudentController : TeacherController
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

            User.Identity.GetUserId();
            var classViewModel = _mappingService.Map<ClassViewModel>(classDomain);

            return View(classViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateAbsenses(ClassViewModel model)
        {
            foreach (var student in model.Students)
            {
                double totalNotExcusedAbsences;
                double totalExcusedAbsences;

                try
                {
                    totalNotExcusedAbsences = student.TotalNotExcusedAbsencesAsFractionNumber.FractionToDoubleNumber();
                    totalExcusedAbsences = double.Parse(student.TotalExcusedAbsences);
                }
                catch (Exception)
                {
                    var errorMsg =
                        $"Некоректно въведени данни за {student.FirstName + " " + student.MiddleName + " " + student.LastName}! След като въведете цялата част оставете интервал след което въведете и дробната";

                    ModelState.AddModelError(string.Empty, errorMsg);
                   
                    return View("Index", model);
                }

                student.TotalNotExcusedAbsences = totalNotExcusedAbsences;

                student.EnteredTotalExcusedAbsences = totalExcusedAbsences;
                student.EnteredTotalNotExcusedAbsences = totalNotExcusedAbsences;
            }

            var studentDomains = _mappingService.Map<List<StudentDomain>>(model.Students);

            _absenceService.CalculateStudentsAbsencesForLastMonth(studentDomains);

            return this.RedirectToAction<StudentController>(x => x.Index(model.EncodedId));
        }
    }
}