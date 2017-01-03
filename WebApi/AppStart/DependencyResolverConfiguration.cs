using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using TemplateProject.DataAccess;
using TemplateProject.DomainModel;

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
            var containerBuilder = new ContainerBuilder();

            BootstrapModules(containerBuilder);

            containerBuilder.RegisterHttpRequestMessage(config);
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = containerBuilder.Build();

            RegisterWriterFactory(container);
            var dependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = dependencyResolver;
        }

        private static void BootstrapModules(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterAssemblyModules(typeof(Entity).Assembly);
            containerBuilder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
        }

        private static void RegisterWriterFactory(IComponentContext container)
        {
            Configuration.WriterFactory = type
                => container.Resolve(typeof(IWriter<>).MakeGenericType(type.GetType())) as IWriter;
        }
    }
}