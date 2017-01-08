using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace TemplateProject.WebAPI.Autofac
{
    /// <summary>
    /// Autofac module that register all model builders in assembly.
    /// </summary>
    public class ModelsModule : Module
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
            var assemblyType = typeof(ModelsModule).GetTypeInfo();

            builder.RegisterAssemblyTypes(assemblyType.Assembly)
                .Where(t => t.Name.EndsWith("Builder")
                    && t.Namespace != null && t.Namespace.Contains(".Models.Builders"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblyType.Assembly)
                .Where(t => t.Name.EndsWith("Factory")
                    && t.Namespace != null && t.Namespace.Contains(".Models.LinksFactories"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblyType.Assembly)
                .Where(t => t.Name.EndsWith("Mapper")
                    && t.Namespace != null && t.Namespace.Contains(".Models.Mappers"))
                .AsImplementedInterfaces();
        }
    }
}