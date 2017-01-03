using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace TemplateProject.DomainModel.AutofacModules
{
    /// <summary>
    /// Autofac module that register all factories in assembly.
    /// </summary>
    public class FactoriesModule : Module
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
            var assemblyType = typeof(FactoriesModule).GetTypeInfo();

            builder.RegisterAssemblyTypes(assemblyType.Assembly)
                .Where(t => t.Name.EndsWith("Factory")
                    && t.Namespace != null && t.Namespace.Contains(".Factories"))
                .AsImplementedInterfaces();
        }
    }
}