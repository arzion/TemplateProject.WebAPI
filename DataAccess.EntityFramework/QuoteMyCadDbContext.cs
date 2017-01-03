using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace TemplateProject.DataAccess.EntityFramework
{
    /// <summary>
    /// The context of the Scrabble.
    /// </summary>
    public class TemplateProjectContext : DbContext
    {
        /// <summary>
        /// Initializes the <see cref="TemplateProjectContext"/> class.
        /// </summary>
        static TemplateProjectContext()
        {
            // To align with current data base schema
            Database.SetInitializer<TemplateProjectContext>(null);

            // To generate base from a models:
            // Database.SetInitializer(new CreateDatabaseIfNotExists<TemplateProjectContext>());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateProjectContext"/> class.
        /// </summary>
        public TemplateProjectContext()
        {
            Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => type.BaseType != null
                    && type.BaseType.IsGenericType
                    && type.Namespace != null
                    && type.Namespace.Contains("TypeConfiguration")
                    && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Disposes the context. The underlying <see cref="T:System.Data.Entity.Core.Objects.ObjectContext" /> is also disposed if it was created
        /// is by this context or ownership was passed to this context when this context was created.
        /// The connection to the database (<see cref="T:System.Data.Common.DbConnection" /> object) is also disposed if it was created
        /// is by this context or ownership was passed to this context when this context was created.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            SqlConnection.ClearAllPools();
            base.Dispose(disposing);
        }
    }
}