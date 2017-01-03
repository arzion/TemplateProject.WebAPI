using Autofac;

namespace TemplateProject.DataAccess.AutofacModules
{
    /// <summary>
    /// Autofac module that register all data access dependencies.
    /// </summary>
    /// <seealso cref="Autofac.Module" />
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
                .RegisterType<TransactionRunner>()
                .As<ITransactionRunner>();
        }
    }
}