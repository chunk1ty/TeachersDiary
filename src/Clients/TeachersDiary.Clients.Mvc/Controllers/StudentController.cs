using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTables.Mvc;
using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.Infrastructure.Constants;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Clients.Mvc.ViewModels.Student;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class StudentController : TeacherController
    {
        private readonly IClassService _classService;
        private readonly IMappingService _mappingService;
        private readonly IStudentService _studentService;

        public StudentController(
            IClassService classService,
            IMappingService mappingService,
           IStudentService studentService)
        {
            _classService = classService;
            _mappingService = mappingService;
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult> Index(string classId)
        {
            // TODO optimize only with one request 
            // loook GetAllByClassId method
            var classDomain = await _classService.GetClassByClassIdAsync(classId);
            var classViewModel = _mappingService.Map<ClassViewModel>(classDomain);

            return View(classViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllByClassId([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel, string classId)
        {
            var classDomain = await _classService.GetClassByClassIdAsync(classId);
            var classViewModel = _mappingService.Map<ClassViewModel>(classDomain);

            //TODO server-side paganation
            // in-memory paganation
            var students = classViewModel.Students.OrderBy(x => x.Number);
                //.Skip(requestModel.Start)
                //.Take(requestModel.Length);

            return Json(new DataTablesResponse(requestModel.Draw, students, classViewModel.Students.Count, classViewModel.Students.Count), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create(string classId)
        {
            if (!Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var model = new CreateStudentViewModel
            {
                ClassId = classId
            };

            return PartialView(PartialViewConstants.StudentCreatePartial, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStudentViewModel model)
        {
            if (!Request.IsAjaxRequest())
            { 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                return PartialView(PartialViewConstants.StudentCreatePartial, model);
            }

            var classDomain = _mappingService.Map<StudentDomain>(model);
            var status = _studentService.Add(classDomain);

            if (!status.IsSuccessful)
            {
                ModelState.AddModelError("", status.Message);
                return PartialView(PartialViewConstants.StudentCreatePartial, model);
            }

            return Content("success");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (Request.IsAjaxRequest())
            {
                // TODO add validation
                var student = await _studentService.GetByIdAsync(id);
                var studentAsVm = _mappingService.Map<CreateStudentViewModel>(student);

                return PartialView(PartialViewConstants.StudentEditPartial, studentAsVm);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateStudentViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!ModelState.IsValid)
            {
                return PartialView(PartialViewConstants.StudentEditPartial, model);
            }

            var classDomain = _mappingService.Map<StudentDomain>(model);
            var status = _studentService.Update(classDomain);

            if (!status.IsSuccessful)
            {
                ModelState.AddModelError("", status.Message);
                return PartialView(PartialViewConstants.StudentEditPartial, model);
            }

            return Content("success");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (!Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // TODO add validation
            var student = await _studentService.GetByIdAsync(id);
            var studentAsVm = _mappingService.Map<CreateStudentViewModel>(student);

            return PartialView(PartialViewConstants.StudentDeletePartial, studentAsVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteStudentById(string id)
        {
            if (!Request.IsAjaxRequest())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            await _studentService.DeleteByIdAsync(id);

            return Content("success");
        }
    }
}