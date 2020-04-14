using Ninject.Web.Common.WebHost;
using RRental.Web.Api.Interfaces;
using RRental.Web.Api.Models.Repositories;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RRental.Web.Api.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(RRental.Web.Api.App_Start.NinjectWebCommon), "Stop")]

namespace RRental.Web.Api.App_Start
{
    using System;
    using System.Web;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
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
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        //private static void AddWebApiSupport(StandardKernel kernel)
        //{
        //    // Support WebAPI
        //    System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
        //    System.Web.Http.GlobalConfiguration.Configuration.Services.Add(typeof(IFilterProvider), new NinjectWebApiFilterProvider(kernel));
        //}

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IInventoryRepository>().To<InventoryRepository>().InRequestScope();
            kernel.Bind<IOrderRepository>().To<OrderRepository>().InRequestScope();
            kernel.Bind<ICustomerRepository>().To<CustomerRepository>().InRequestScope();
        }        
    }
}
