using System.Configuration;
using Autofac;
using TemplateProject.DataAccess;
using TemplateProject.DataAccess.EntityFramework;
using TemplateProject.DataAccess.StaticStorage;
using TemplateProject.DataAccess.UnitOfWork;

namespace TemplateProject.WebAPI.AutofacModules
{
    /// <summary>
    /// Autofac module that register all data access dependencies.
    /// </summary>
    public class DataAccessModule : Module
    {
        public const string EfTransactions = "efTransactions";
        public const string EfImmidiate = "efImmidiate";
        public const string Static = "static";

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

            var strategy = ConfigurationManager.AppSettings["dataAccessStrategy"];
            switch (strategy)
            {
                case EfTransactions:
                {
                    RegisterEf(builder);
                    break;
                }
                case EfImmidiate:
                {
                    RegisterEfImmidiate(builder);
                    break;
                }
                case Static:
                {
                    RegisterStatic(builder);
                    break;
                }
                default:
                {
                    throw new ConfigurationErrorsException(
                        $"dataAccessStrategy setting value incorrect. Supported values are: {EfTransactions}, {EfImmidiate}, {Static}");
                }
            }
        }

        private void RegisterEf(ContainerBuilder builder)
        {
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

        private void RegisterEfImmidiate(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(EfImmidiateWriter<>))
                .As(typeof(IWriter<>));

            builder
                .RegisterGeneric(typeof(EfReader<>))
                .As(typeof(IReader<>));

            builder
                .RegisterType<DbContextProvider>()
                .As<IDbContextProvider>();
        }

        private void RegisterStatic(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(StaticWriter<>))
                .As(typeof(IWriter<>));

            builder
                .RegisterGeneric(typeof(StaticReader<>))
                .As(typeof(IReader<>));
        }
    }
}