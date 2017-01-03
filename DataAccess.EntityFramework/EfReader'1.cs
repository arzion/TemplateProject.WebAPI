using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess.EntityFramework
{
    /// <summary>
    /// The reader of the Domain Entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="TemplateProject.DataAccess.IReader{TEntity}" />
    public class EfReader<TEntity> : IReader<TEntity> where TEntity : Entity
    {
        private readonly DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfReader{TEntity}" /> class.
        /// </summary>
        /// <param name="provider">The factory of <see cref="DbContext"/>.</param>
        public EfReader(IDbContextProvider provider)
        {
            _context = provider.GetDbContext();
        }

        private IDbSet<TEntity> Entities => _context.Set<TEntity>();

        /// <summary>
        /// Finds the entity by specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>
        /// The found <see cref="TEntity" />
        /// </returns>
        public virtual async Task<TEntity> FindAsync(int id)
        {
            return await Entities.FirstOrDefaultAsync(it => it.Id == id);
        }

        /// <summary>
        /// Finds all entities.
        /// </summary>
        /// <returns>
        /// The list of <see cref="TEntity" />
        /// </returns>
        public virtual async Task<List<TEntity>> FindAllAsync(PagingArgs pagingArgs = null)
        {
            pagingArgs = pagingArgs ?? PagingArgs.Default;

            return await Entities
                .OrderBy(it => it.Id)
                .Skip(pagingArgs.PageSize * pagingArgs.PageIndex)
                .Take(pagingArgs.PageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Finds entities by the criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="pagingArgs">The paging arguments.</param>
        /// <returns>
        /// The list of <see cref="TEntity" /> found by criteria.
        /// </returns>
        public virtual async Task<List<TEntity>> FindByCriteriaAsync(
            Expression<Func<TEntity, bool>> criteria,
            PagingArgs pagingArgs = null)
        {
            pagingArgs = pagingArgs ?? PagingArgs.Default;

            return await Entities
                .Where(criteria)
                .OrderBy(it => it.Id)
                .Skip(pagingArgs.PageSize * pagingArgs.PageIndex)
                .Take(pagingArgs.PageSize)
                .ToListAsync();
        }
    }
}