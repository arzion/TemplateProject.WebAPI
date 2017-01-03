using System.Threading.Tasks;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess
{
    /// <summary>
    /// The writer to update the <see cref="Entity" />.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="System.IDisposable" />
    public interface IWriter<in TEntity> : IWriter where TEntity : Entity
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The identifier of the updated entity.</returns>
        Task<int> AddAsync(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task UpdateAsync(TEntity entity);
    }
}