using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using MediatR;
using MediatR.Pipeline;
using global::Ninject.Extensions.Conventions;
using global::Ninject.Planning.Bindings.Resolvers;
using Mediator.Models;

namespace Mediator
{
    public class WebApiApplication : NinjectHttpApplication
    {
        protected new void Application_Start()
        {
            base.Application_Start();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private void RegisterServices(StandardKernel kernel)
        {
            kernel.Bind(scan => scan.FromAssemblyContaining<IMediator>().SelectAllClasses().BindDefaultInterface());
            kernel.Bind(scan => scan.FromAssemblyContaining<Cliente>().SelectAllClasses().InheritedFrom(typeof(IRequestHandler<,>)).BindAllInterfaces());
            kernel.Bind(scan => scan.FromAssemblyContaining<Cliente>().SelectAllClasses().InheritedFrom(typeof(IRequestHandler<>)).BindAllInterfaces());
            kernel.Bind(scan => scan.FromAssemblyContaining<Cliente>().SelectAllClasses().InheritedFrom(typeof(IAsyncRequestHandler<,>)).BindAllInterfaces());
            kernel.Bind(scan => scan.FromAssemblyContaining<Cliente>().SelectAllClasses().InheritedFrom(typeof(IAsyncRequestHandler<>)).BindAllInterfaces());
            kernel.Bind(scan => scan.FromAssemblyContaining<Cliente>().SelectAllClasses().InheritedFrom(typeof(ICancellableAsyncRequestHandler<,>)).BindAllInterfaces());
            kernel.Bind(scan => scan.FromAssemblyContaining<Cliente>().SelectAllClasses().InheritedFrom(typeof(ICancellableAsyncRequestHandler<>)).BindAllInterfaces());
            kernel.Bind(scan => scan.FromAssemblyContaining<Cliente>().SelectAllClasses().InheritedFrom(typeof(INotificationHandler<>)).BindAllInterfaces());
            kernel.Bind(scan => scan.FromAssemblyContaining<Cliente>().SelectAllClasses().InheritedFrom(typeof(IAsyncNotificationHandler<>)).BindAllInterfaces());
            kernel.Bind(scan => scan.FromAssemblyContaining<Cliente>().SelectAllClasses().InheritedFrom(typeof(ICancellableAsyncNotificationHandler<>)).BindAllInterfaces());

            //Pipeline
            kernel.Bind(typeof(IPipelineBehavior<,>)).To(typeof(RequestPreProcessorBehavior<,>));
            kernel.Bind(typeof(IPipelineBehavior<,>)).To(typeof(RequestPostProcessorBehavior<,>));

            kernel.Bind<SingleInstanceFactory>().ToMethod(ctx => t => ctx.Kernel.TryGet(t));
            kernel.Bind<MultiInstanceFactory>().ToMethod(ctx => t => ctx.Kernel.GetAll(t));
        }
    }
}
