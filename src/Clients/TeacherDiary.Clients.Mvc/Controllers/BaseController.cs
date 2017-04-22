using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Ninject;
using TeacherDiary.Clients.Mvc.Infrastructure.Mapping;

namespace TeacherDiary.Clients.Mvc.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected IMapper Mapper => AutoMapperConfig.Configuration.CreateMapper();
    }
}