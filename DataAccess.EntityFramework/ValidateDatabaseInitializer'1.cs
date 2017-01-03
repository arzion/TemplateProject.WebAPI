using System;
using System.Data.Entity;

namespace TemplateProject.DataAccess.EntityFramework
{
    /// <summary>
    /// The strategy of initializing Database that only validates the Database.
    /// </summary>
    /// <typeparam name="TContext">The type of the context.</typeparam>
    public class ValidateDatabaseInitializer<TContext> : IDatabaseInitializer<TContext>
        where TContext : DbContext
    {
        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.InvalidOperationException">The database is not compatible with the entity model or not created.</exception>
        public void InitializeDatabase(TContext context)
        {
            if (!context.Database.Exists() || !context.Database.CompatibleWithModel(true))
            {
                throw new InvalidOperationException(
                    "The database is not compatible with the entity model or not created.");
            }
        }
    }
}