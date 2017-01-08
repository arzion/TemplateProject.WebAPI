using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using Autofac;
using Autofac.Integration.WebApi;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Windsor.Proxy;
using TemplateProject.WebAPI.Controllers;
using TemplateProject.WebAPI.Utils;

namespace TemplateProject.WebAPI.AppStart
{
    /// <summary>
    /// Registration of the dependency resolver of the ASP.NET Web.API application.
    /// </summary>
    public static class DependencyResolverConfiguration
    {
        /// <summary>
        /// Registers the dependency resolver.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void RegisterResolver(HttpConfiguration config)
        {
            var ioc = ConfigurationManager.AppSettings["iocContainer"];
            switch (ioc)
            {
                case "castle":
                {
                    ConfigureCastleWindsorDependencyResolver(config);
                    break;
                }
                case "autofac":
                {
                    ConfigureAutofacDependencyResolver(config);
                    break;
                }
                default:
                {
                    throw new ConfigurationErrorsException("iocContainer setting is incorrect");
                }
            }
        }

        private static void ConfigureAutofacDependencyResolver(HttpConfiguration config)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyModules(typeof(HomeController).Assembly);

            containerBuilder.RegisterHttpRequestMessage(config);
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = containerBuilder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void ConfigureCastleWindsorDependencyResolver(HttpConfiguration config)
        {
            var container =
                new WindsorContainer(
                    new DefaultKernel(
                        new InlineDependenciesPropagatingDependencyResolver(),
                        new DefaultProxyFactory()),
                    new DefaultComponentInstaller());

            container.Install(FromAssembly.This());

            container.Register(
                Classes
                    .FromThisAssembly()
                    .BasedOn<ApiController>()
                    .Configure(c => c.PropertiesIgnore(it => true))
                    .LifestylePerWebRequest());

            config.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(container));
        }
    }
}