using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

using TeacherDiary.Clients.Mvc;
using TeacherDiary.Data.Contracts;
using TeacherDiary.Data.Ef;
using TeacherDiary.Data.Ef.Contracts;
using TeacherDiary.Data.Ef.Repositories;
using TeacherDiary.Data.Services;
using TeacherDiary.Data.Services.Contracts;
using TeacherDiary.Services;
using TeacherDiary.Services.Contracts;
using TeacherDiary.Services.Identity;
using TeacherDiary.Services.Identity.Contracts;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectConfig), "Stop")]

namespace TeacherDiary.Clients.Mvc
{
    [ExcludeFromCodeCoverage]
    public static class NinjectConfig 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(ITeacherDiaryDbContext),
                    typeof(ITeacherDiaryDbContextSaveChanges))
                .ToMethod(ctx => ctx.Kernel.Get<TeacherDiaryDbContext>())
                .InRequestScope();
           
            kernel.Bind<IClassRepository>().To<EfClassRepository>();
            kernel.Bind<IClassService>().To<ClassService>();

            kernel.Bind<IAbsenceService>().To<AbsenceService>();

            kernel.Bind<IExelParser>().To<ExelParser>();

            kernel.Bind<IIdentitySignInService>().ToMethod(_ => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>());
            kernel.Bind<IIdentityUserManagerService>().ToMethod(_ => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
        }        
    }
}
