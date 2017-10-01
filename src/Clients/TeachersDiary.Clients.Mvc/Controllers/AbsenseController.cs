using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;
using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Common.Extensions;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class AbsenseController : TeacherController
    {
        private const int SeptemberId = 9;
        private readonly IAbsenceService _absenceService;
        private readonly IMappingService _mappingService;
        private readonly IMonthService _monthService;
        private readonly IClassService _classService;

        public AbsenseController(IAbsenceService absenceService, IMappingService mappingService, IClassService classService, IMonthService monthService)
        {
            _absenceService = absenceService;
            _mappingService = mappingService;
            _classService = classService;
            _monthService = monthService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string classId)
        {
            var classDomain = await _classService.GetClassByClassIdAsync(classId);
           
            var classViewModel = _mappingService.Map<ClassViewModel>(classDomain);
            classViewModel.AvailableMonths = _monthService.GetCurrentAndPreviousMonth();

            return View(classViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateAbsenses(ClassViewModel model, string month)
        {
            if (!IsValidRequest(model, month))
            {
                model.AvailableMonths = _monthService.GetCurrentAndPreviousMonth();
                return View("Index", model);
            }

            var studentDomains = _mappingService.Map<List<StudentDomain>>(model.Students);

            _absenceService.CalculateStudentAbsences(studentDomains, month);

            return this.RedirectToAction<AbsenseController>(x => x.Index(model.Id));
        }

        private bool IsValidRequest(ClassViewModel model, string month)
        {
            foreach (var student in model.Students)
            {
                if (student.TotalExcusedAbsences.IsDoubleNumber() && student.TotalNotExcusedAbsences.IsFractionNumber())
                {
                    continue;
                }

                var errorMsg =
                    $"Некоректно въведени данни за {student.FirstName + " " + student.MiddleName + " " + student.LastName}. Извинените отсъствия трябва да са цяло число, а за неизвинените след като въведете цялата част оставете интервал след което въведете и дробната. Дробната част се въвежда с '/' разделител.";

                ModelState.AddModelError(string.Empty, errorMsg);

                return false;
            }

            // if we try to calculate absences for the future
            // example
            // sep october
            // i don't have any absenses for sep but i select october
            var selectedMonth = int.Parse(month);
            if (selectedMonth == SeptemberId)
            {
                return true;
            }

            selectedMonth--;
           
            if (!model.Students.FirstOrDefault().Absences.Any(x => x.MonthId == selectedMonth))
            {
                ModelState.AddModelError(string.Empty, Resources.Resources.CannotCalculaetAbsencesForTheSelectedMonth);

                return false;
            }

            return true;
        }
    }
}