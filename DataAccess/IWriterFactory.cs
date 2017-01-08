using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess
{
    /// <summary>
    /// The factory to create the Writer for paricular entity.
    /// </summary>
    public interface IWriterFactory
    {
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The writer of particular entity.</returns>
        IWriter Create(Entity entity);
    }
}