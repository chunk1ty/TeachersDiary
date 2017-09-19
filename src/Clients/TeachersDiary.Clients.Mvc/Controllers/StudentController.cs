using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataTables.Mvc;
using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.Infrastructure.Attribute;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Clients.Mvc.ViewModels.Student;
using TeachersDiary.Common.Enumerations;
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
            if (Request.IsAjaxRequest())
            {
                var model = new CreateStudentViewModel
                {
                    ClassId = classId
                };

                return PartialView("_CreatePartial", model);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateStudentViewModel model)
        {
            if (Request.IsAjaxRequest())
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_CreatePartial", model);
                }

                try
                {
                    var classDomain = _mappingService.Map<StudentDomain>(model);
                    _studentService.Add(classDomain);
                }
                catch (Exception  ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
                    {
                        ModelState.AddModelError("", $"Ученик с номер {model.Number} вече съществува!");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не успешно добавяне. Моля свържете се със училищният администратор.");
                    }
                    
                    return PartialView("_CreatePartial", model);
                }

                return Content("success");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (Request.IsAjaxRequest())
            {
                // TODO add validation
                var student = await _studentService.GetByIdAsync(id);
                var studentAsVm = _mappingService.Map<CreateStudentViewModel>(student);

                return PartialView("_EditPartial", studentAsVm);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateStudentViewModel model)
        {
            if (Request.IsAjaxRequest())
            {
                if (!ModelState.IsValid)
                {
                    return PartialView("_EditPartial", model);
                }

                try
                {
                    var classDomain = _mappingService.Map<StudentDomain>(model);
                    _studentService.Update(classDomain);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.InnerException.Message.Contains("Cannot insert duplicate key row in object"))
                    {
                        ModelState.AddModelError("", $"Ученик с номер {model.Number} вече съществува!");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Не успешно променяне. Моля свържете се със училищният администратор.");
                    }

                    return PartialView("_EditPartial", model);
                }

               

                return Content("success");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (Request.IsAjaxRequest())
            {
                // TODO add validation
                var student = await _studentService.GetByIdAsync(id);
                var studentAsVm = _mappingService.Map<CreateStudentViewModel>(student);

                return PartialView("_DeletePartial", studentAsVm);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteStudentById(string id)
        {
            if (Request.IsAjaxRequest())
            {
                await _studentService.DeleteByIdAsync(id);

                return Content("success");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}