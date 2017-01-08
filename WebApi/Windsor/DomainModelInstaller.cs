using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TemplateProject.DomainModel;

namespace TemplateProject.WebAPI.Windsor
{
    /// <summary>
    /// Autofac module that register all domain module services dependencies.
    /// </summary>
    public class DomainModelInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Performs the installation in the <see cref="T:Castle.Windsor.IWindsorContainer" />.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var assemblyType = typeof(Entity).GetTypeInfo();

            container.Register(Classes.FromAssembly(assemblyType.Assembly)
                .Where(t => t.Name.EndsWith("Factory")
                    && t.Namespace != null
                    && t.Namespace.Contains(".Factories"))
                .WithServiceAllInterfaces()
                .LifestyleTransient());
        }
    }
}