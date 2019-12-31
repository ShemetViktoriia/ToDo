using Autofac;
using Autofac.Integration.Web;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using ToDo_BLL.Services;
using ToDo_DAL;
using ToDo_Repository;

namespace ToDo_WebUI
{
    public class Global : HttpApplication, IContainerProviderAccessor
    {
        // Provider that holds the application container.
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        void Application_Start(object sender, EventArgs e)
        {
            // Build up your application container and register your dependencies.
            var builder = new ContainerBuilder();
            builder.RegisterType<ToDoContext>()
                   .As<DbContext>()
                   .InstancePerLifetimeScope();
            builder.RegisterType(typeof(ToDoItemRepository)).As(typeof(IToDoItemRepository)).InstancePerLifetimeScope();
            builder.RegisterType(typeof(ToDoItemService)).As(typeof(IToDoItemService)).InstancePerLifetimeScope();

            // Once you're done registering things, set the container
            // provider up with your registrations.
            _containerProvider = new ContainerProvider(builder.Build());

            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}