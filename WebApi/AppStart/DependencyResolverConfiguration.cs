using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using TemplateProject.WebApi.Controllers;

namespace TemplateProject.WebApi.AppStart
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
            ConfigureAutofacDependencyResolver(config);
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
    }
}