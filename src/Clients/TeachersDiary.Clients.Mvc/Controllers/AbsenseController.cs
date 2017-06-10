using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Common.Extensions;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;


namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class AbsenseController : TeacherController
    {
        private readonly IAbsenceService _absenceService;
        private readonly IMappingService _mappingService;

        public AbsenseController(IAbsenceService absenceService, IMappingService mappingService)
        {
            _absenceService = absenceService;
            _mappingService = mappingService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateAbsenses(ClassViewModel model)
        {
            foreach (var student in model.Students)
            {
                if (student.TotalExcusedAbsences.IsDoubleNumber() && student.TotalNotExcusedAbsences.IsFractionNumber())
                {
                    continue;
                }

                var errorMsg =
                    $"Некоректно въведени данни за {student.FirstName + " " + student.MiddleName + " " + student.LastName}! След като въведете цялата част оставете интервал след което въведете и дробната";

                ModelState.AddModelError(string.Empty, errorMsg);

                return View("~/Views/Class/Index.cshtml", model);
            }

            var studentDomains = _mappingService.Map<List<StudentDomain>>(model.Students);

            _absenceService.CalculateStudentsAbsencesForLastMonth(studentDomains);

            return this.RedirectToAction<ClassController>(x => x.Index(model.EncodedId));
        }
    }
}