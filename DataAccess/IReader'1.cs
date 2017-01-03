using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess
{
    /// <summary>
    /// The reader of the TEntity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IReader<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Finds the entity by specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The found <see cref="TEntity"/></returns>
        Task<TEntity> FindAsync(int id);

        /// <summary>
        /// Finds all entities.
        /// </summary>
        /// <param name="pagingArgs">The paging arguments.</param>
        /// <returns>
        /// The list of <see cref="TEntity" />
        /// </returns>
        Task<List<TEntity>> FindAllAsync(PagingArgs pagingArgs = null);

        /// <summary>
        /// Finds entities by the criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="pagingArgs">The paging arguments.</param>
        /// <returns>
        /// The list of <see cref="TEntity" /> found by criteria.
        /// </returns>
        Task<List<TEntity>> FindByCriteriaAsync(
            Expression<Func<TEntity, bool>> criteria,
            PagingArgs pagingArgs = null);
    }
}