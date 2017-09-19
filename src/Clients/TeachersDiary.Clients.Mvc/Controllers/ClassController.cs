﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Expressions;

using TeachersDiary.Clients.Mvc.Controllers.Abstracts;
using TeachersDiary.Clients.Mvc.Infrastructure.Attribute;
using TeachersDiary.Clients.Mvc.ViewModels.Class;
using TeachersDiary.Common.Enumerations;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Domain;
using TeachersDiary.Services.Contracts;
using TeachersDiary.Services.Contracts.Mapping;

namespace TeachersDiary.Clients.Mvc.Controllers
{
    public class ClassController : TeacherController
    {
        private readonly IClassService _classService;
        private readonly IUserService _userService;
        private readonly ILoggingService _loggingService;
        private readonly IMappingService _mappingService;

        public ClassController(
            IClassService classService, 
            IMappingService mappingService, 
            IUserService userService, 
            ILoggingService loggingService)
        {
            _classService = classService;
            _mappingService = mappingService;
            _userService = userService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public async Task<ActionResult> All()
        {
            var classDomains =  await _classService.GetClassesBySchoolIdAsync(1);

            var classViewModels = _mappingService.Map<IList<ClassViewModel>>(classDomains);

            return View(classViewModels);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string classId)
        {
            await _classService.DeleteByIdAsync(classId);

            return this.RedirectToAction<ClassController>(x => x.All());
        }

        [HttpGet]
        [TeachersDiaryAuthorize(ApplicationRoles.SchoolAdministrator)]
        public async Task<ActionResult> Create()
        {
            var createClassViewModel = new CreateClassViewModel();

            var teachers = await _userService.GetTeachersBySchoolIdAsync();
            createClassViewModel.Teachers = new SelectList(teachers, "Id", "FullName", 1);

            return View(createClassViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [TeachersDiaryAuthorize(ApplicationRoles.SchoolAdministrator)]
        public async Task<ActionResult> Create(CreateClassViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var teachers = await _userService.GetTeachersBySchoolIdAsync();
                model.Teachers = new SelectList(teachers, "Id", "FullName", 1);

                return View(model);
            }

            try
            {
                var classDomain = _mappingService.Map<ClassDomain>(model);
                _classService.Add(classDomain);
            }
            catch (Exception ex)
            {
                _loggingService.Error(ex.Message, ex);

                ModelState.AddModelError("", Resources.Resources.ErrorOnCreationOfNewClass);
                return View(model);
            }

            return this.RedirectToAction<ClassController>(x => x.All());
        }
    }
}