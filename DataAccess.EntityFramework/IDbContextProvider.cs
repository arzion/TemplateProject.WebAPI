using System.Data.Entity;

namespace TemplateProject.DataAccess.EntityFramework
{
    /// <summary>
    /// The factory that creates <see cref="DbContext"/> instance.
    /// </summary>
    public interface IDbContextProvider
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <returns>Created DB context.</returns>
        DbContext GetDbContext();
    }
}