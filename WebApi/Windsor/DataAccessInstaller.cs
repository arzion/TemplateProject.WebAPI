using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TemplateProject.DataAccess;
using TemplateProject.DataAccess.EntityFramework;
using TemplateProject.DataAccess.StaticStorage;
using TemplateProject.DataAccess.UnitOfWork;

namespace TemplateProject.WebAPI.Windsor
{
    /// <summary>
    /// Autofac module that register all data access dependencies.
    /// </summary>
    public class DataAccessInstaller : IWindsorInstaller
    {
        public const string EfTransactions = "efTransactions";
        public const string EfImmidiate = "efImmidiate";
        public const string Static = "static";

        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        /// <exception cref="ConfigurationErrorsException">dataAccessStrategy setting value incorrect. Supported values are: {EfTransactions}, {EfImmidiate}, {Static}");</exception>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var strategy = ConfigurationManager.AppSettings["dataAccessStrategy"];

            container
                .Register(Component
                    .For<ITransactionRunner>()
                    .ImplementedBy<TransactionRunner>()
                    .LifestyleTransient());

            if (strategy != EfTransactions)
            {
                container
                    .Register(Component
                        .For<IUnitOfWorkProcessor>()
                        .ImplementedBy<DefaultUnitOfWorkProcessor>()
                        .LifestyleTransient());
            }

            container
                .Register(Component
                    .For<IWriterFactory>()
                    .UsingFactoryMethod((kernel, context) => new WindsorWriterFactory(kernel))
                    .LifestyleTransient());

            switch (strategy)
            {
                case EfTransactions:
                {
                    RegisterEf(container);
                    break;
                }
                case EfImmidiate:
                {
                    RegisterEfImmidiate(container);
                    break;
                }
                case Static:
                {
                    RegisterStatic(container);
                    break;
                }
                default:
                {
                    throw new ConfigurationErrorsException(
                        $"dataAccessStrategy setting value incorrect. Supported values are: {EfTransactions}, {EfImmidiate}, {Static}");
                }
            }
        }

        private void RegisterEf(IWindsorContainer container)
        {
            container
                .Register(Component
                    .For(typeof(IWriter<>))
                    .ImplementedBy(typeof(EfWriter<>))
                    .LifestyleTransient());

            container
                .Register(Component
                    .For(typeof(IReader<>))
                    .ImplementedBy(typeof(EfReader<>))
                    .LifestyleTransient());

            container
                .Register(Component
                    .For(typeof(IUnitOfWorkProcessor))
                    .ImplementedBy(typeof(EfUnitOfWorkProcessor))
                    .LifestyleTransient());

            container
                .Register(Component
                    .For(typeof(IDbContextProvider))
                    .ImplementedBy(typeof(DbContextProvider))
                    .LifestylePerWebRequest());
        }

        private void RegisterEfImmidiate(IWindsorContainer container)
        {
            container
                .Register(Component
                    .For(typeof(IWriter<>))
                    .ImplementedBy(typeof(EfImmidiateWriter<>))
                    .LifestyleTransient());

            container
                .Register(Component
                    .For(typeof(IReader<>))
                    .ImplementedBy(typeof(EfReader<>))
                    .LifestyleTransient());

            container
                .Register(Component
                    .For(typeof(IDbContextProvider))
                    .ImplementedBy(typeof(DbContextProvider))
                    .LifestyleTransient());
        }

        private void RegisterStatic(IWindsorContainer container)
        {
            container
                .Register(Component
                    .For(typeof(IWriter<>))
                    .ImplementedBy(typeof(StaticWriter<>))
                    .LifestyleTransient());

            container
                .Register(Component
                    .For(typeof(IReader<>))
                    .ImplementedBy(typeof(StaticReader<>))
                    .LifestyleTransient());
        }
    }
}