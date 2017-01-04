using Autofac;
using TemplateProject.DataAccess;
using TemplateProject.DataAccess.EntityFramework;
using TemplateProject.DataAccess.UnitOfWork;

namespace TemplateProject.WebAPI.AutofacModules
{
    /// <summary>
    /// Autofac module that register all data access dependencies based on entity framework data access.
    /// </summary>
    public class EfDataAccessModule : Module
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
            builder.RegisterModule<DataAccess.AutofacModules.DataAccessModule>();

            builder
                .RegisterGeneric(typeof(EfWriter<>))
                .As(typeof(IWriter<>));

            builder
                .RegisterGeneric(typeof(EfReader<>))
                .As(typeof(IReader<>));

            builder
                .RegisterType<EfUnitOfWorkProcessor>()
                .As<IUnitOfWorkProcessor>();

            builder
                .RegisterType<DbContextProvider>()
                .As<IDbContextProvider>()
                .InstancePerRequest();
        }
    }
}