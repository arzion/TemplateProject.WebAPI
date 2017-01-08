using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace TemplateProject.WebAPI.Windsor
{
    /// <summary>
    /// Autofac module that register all model builders in assembly.
    /// </summary>
    public class ModelsInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var assemblyType = typeof(ModelsInstaller).GetTypeInfo();

            container.Register(Classes.FromAssembly(assemblyType.Assembly)
                .Where(t => t.Name.EndsWith("Builder")
                    && t.Namespace != null
                    && t.Namespace.Contains(".Models.Builders"))
                .WithServiceAllInterfaces()
                .LifestyleTransient());

            container.Register(Classes.FromAssembly(assemblyType.Assembly)
                .Where(t => t.Name.EndsWith("Factory")
                    && t.Namespace != null
                    && t.Namespace.Contains(".Models.LinksFactories"))
                .WithServiceAllInterfaces()
                .LifestyleTransient());

            container.Register(Classes.FromAssembly(assemblyType.Assembly)
                .Where(t => t.Name.EndsWith("Mapper")
                    && t.Namespace != null
                    && t.Namespace.Contains(".Models.Mappers"))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
        }
    }
}