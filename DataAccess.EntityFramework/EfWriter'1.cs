using System.Data.Entity;
using System.Threading.Tasks;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess.EntityFramework
{
    /// <summary>
    /// The writer to update the <see cref="Entity" />.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class EfWriter<TEntity> : IWriter<TEntity> where TEntity : Entity
    {
        private readonly DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfWriter{TEntity}"/> class.
        /// </summary>
        /// <param name="provider">The factory of <see cref="DbContext"/>.</param>
        public EfWriter(IDbContextProvider provider)
        {
            _context = provider.GetDbContext();
        }

        private DbSet Entities => _context.Set(typeof(TEntity));

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual Task<int> AddAsync(TEntity entity)
        {
            Entities.Add(entity);
            return Task.FromResult(entity.Id);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual Task DeleteAsync(TEntity entity)
        {
            Entities.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual Task UpdateAsync(TEntity entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}