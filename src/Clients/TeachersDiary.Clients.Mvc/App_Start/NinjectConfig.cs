using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Syntax;
using Ninject.Web.Common;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;

using TeachersDiary.Clients.Mvc;
using TeachersDiary.Data.Ef;
using TeachersDiary.Data.Ef.Contracts;
using TeachersDiary.Data.Ef.GenericRepository;
using TeachersDiary.Data.Identity;
using TeachersDiary.Data.Identity.Contracts;
using TeachersDiary.Data.Services;
using TeachersDiary.Data.Services.Contracts;
using TeachersDiary.Services.ExcelParser;
using TeachersDiary.Services.Mapping;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectConfig), "Stop")]

namespace TeachersDiary.Clients.Mvc
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
        public static IKernel CreateKernel()
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
            RegisterDataModule(kernel);

            kernel.Bind<IIdentitySignInService>().ToMethod(_ => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>());
            kernel.Bind<IIdentityUserManagerService>().ToMethod(_ => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());

            kernel.Bind(x => x
                .FromAssembliesMatching("TeachersDiary.Service*")
                .SelectAllClasses()
                .BindDefaultInterface());
        }

        private static void RegisterDataModule(IKernel kernel)
        {
            kernel.Bind(typeof(ITeachersDiaryDbContext), typeof(IUnitOfWork))
                .ToMethod(ctx => ctx.Kernel.Get<TeachersDiaryDbContext>())
                .InRequestScope();

            kernel.Bind(typeof(IEntityFrameworkGenericRepository<>)).To(typeof(EntityFrameworkGenericRepository<>));
            kernel.Bind(typeof(IQuerySettings<>)).To(typeof(QuerySettings<>));

            kernel.Bind(x => x
                    .FromAssembliesMatching("TeachersDiary.Data.Services*")
                    .SelectAllClasses()
                    .BindDefaultInterface());
        }
    }
}
