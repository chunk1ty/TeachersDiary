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
using TeachersDiary.Services.Contracts.Mapping;


namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class AbsenseController : TeacherController
    {
        private readonly IAbsenceService _absenceService;
        private readonly IMappingService _mappingService;
        private readonly IClassService _classService;

        public AbsenseController(IAbsenceService absenceService, IMappingService mappingService, IClassService classService)
        {
            _absenceService = absenceService;
            _mappingService = mappingService;
            _classService = classService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string classId)
        {
            var classDomain = await _classService.GetClassByClassIdAsync(classId);
           
            var classViewModel = _mappingService.Map<ClassViewModel>(classDomain);
            classViewModel.AvailableMonths = AvailableMonths();

            return View(classViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateAbsenses(ClassViewModel model, string month)
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

                return View("~/Views/Absense/Index.cshtml", model);
            }

            var studentDomains = _mappingService.Map<List<StudentDomain>>(model.Students);

            _absenceService.CalculateStudentAbsences(studentDomains, month);

            return this.RedirectToAction<AbsenseController>(x => x.Index(model.Id));
        }

        private List<Month> AvailableMonths()
        {
            var monthId = DateTime.UtcNow.Month;

            var months = new List<Month>();

            months.Add(GetMonth(monthId));
            months.Add(monthId != 12 ? GetMonth(monthId + 1) : GetMonth(1));

            return months;
        }

        private Month GetMonth(int monthId)
        {
            switch (monthId)
            {
                case 9:
                    return new Month(9, "Септември");
                case 10:
                    return new Month(10, "Октомври");
                case 11:
                    return new Month(11, "Ноември");
                case 12:
                    return new Month(12, "Декември");
                case 1:
                    return new Month(13, "Януари");
                case 2:
                    return new Month(14, "Февруари");
                case 3:
                    return new Month(15, "Март");
                case 4:
                    return new Month(16, "Април");
                case 5:
                    return new Month(17, "Май");
                case 6:
                    return new Month(18, "Юни");
                default:
                    throw new InvalidOperationException(nameof(monthId));
            }
        }
    }

    public class Month
    {
        public Month(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}