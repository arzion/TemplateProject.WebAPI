using Autofac;
using TemplateProject.DataAccess;
using TemplateProject.DataAccess.EntityFramework;

namespace TemplateProject.WebAPI.AutofacModules
{
    /// <summary>
    /// Autofac module that register all utils dependencies.
    /// </summary>
    public class DataAccessModule : Module
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
            builder
                .RegisterGeneric(typeof(EfWriter<>))
                .As(typeof(IWriter<>));

            builder
                .RegisterGeneric(typeof(EfReader<>))
                .As(typeof(IReader<>));

            builder
                .RegisterType<DbContextProvider>()
                .As<IDbContextProvider>()
                .InstancePerRequest();
        }
    }
}