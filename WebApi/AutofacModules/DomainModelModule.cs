using Autofac;
using System.Reflection;
using TemplateProject.DomainModel;
using Module = Autofac.Module;

namespace TemplateProject.WebAPI.AutofacModules
{
    /// <summary>
    /// Autofac module that register all domain module services dependencies.
    /// </summary>
    public class DomainModelModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            var assemblyType = typeof(Entity).GetTypeInfo();

            builder.RegisterAssemblyTypes(assemblyType.Assembly)
                .Where(t => t.Name.EndsWith("Factory")
                    && t.Namespace != null
                    && t.Namespace.Contains(".Factories"))
                .AsImplementedInterfaces();
        }
    }
}