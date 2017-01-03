using Autofac;
using TemplateProject.DataAccess;
using TemplateProject.DataAccess.StaticStorage;

namespace TemplateProject.WebAPI.AutofacModules
{
    /// <summary>
    /// Autofac module that register all data access dependencies.
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
            builder.RegisterModule<DataAccess.AutofacModules.DataAccessModule>();

            //builder
            //    .RegisterGeneric(typeof(EfWriter<>))
            //    .As(typeof(IWriter<>));

            //builder
            //    .RegisterGeneric(typeof(EfReader<>))
            //    .As(typeof(IReader<>));

            //builder
            //    .RegisterType<DbContextProvider>()
            //    .As<IDbContextProvider>()
            //    .InstancePerRequest();

            builder
                .RegisterGeneric(typeof(StaticWriter<>))
                .As(typeof(IWriter<>));

            builder
                .RegisterGeneric(typeof(StaticReader<>))
                .As(typeof(IReader<>));
        }
    }
}