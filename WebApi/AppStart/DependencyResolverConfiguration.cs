using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Autofac;
using Autofac.Integration.WebApi;
using TemplateProject.DataAccess;
using TemplateProject.DataAccess.UnitOfWork;
using TemplateProject.WebAPI.Controllers;

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
            var dependencyResolver = ConfigureAutofacDependencyResolver(config);
            ConfigureDataAccess(dependencyResolver);

            config.DependencyResolver = dependencyResolver;
        }

        private static IDependencyResolver ConfigureAutofacDependencyResolver(HttpConfiguration config)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterAssemblyModules(typeof(HomeController).Assembly);

            containerBuilder.RegisterHttpRequestMessage(config);
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = containerBuilder.Build();

            return new AutofacWebApiDependencyResolver(container);
        }

        private static void ConfigureDataAccess(IDependencyResolver resolver)
        {
            Configuration.WriterFactory =
                type => resolver.GetService(typeof(IWriter<>).MakeGenericType(type.GetType())) as IWriter;

            // Use in case of EfDataAccessModule registration
            Configuration.UnitOfWorkProcessorFactory =
                () => resolver.GetService(typeof(IUnitOfWorkProcessor)) as IUnitOfWorkProcessor;
        }
    }
}