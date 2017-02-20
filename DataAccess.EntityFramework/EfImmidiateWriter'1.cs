using System.Data.Entity;
using System.Threading.Tasks;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess.EntityFramework
{
    /// <summary>
    /// The writer to update the <see cref="Entity" /> immidiately after calling IWriter methods.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class EfImmidiateWriter<TEntity> : IWriter<TEntity> where TEntity : Entity
    {
        private readonly DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfImmidiateWriter{TEntity}"/> class.
        /// </summary>
        /// <param name="provider">The factory of <see cref="DbContext"/>.</param>
        public EfImmidiateWriter(IDbContextProvider provider)
        {
            _context = provider.GetDbContext();
        }

        private DbSet Entities => _context.Set(typeof(TEntity));

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual async Task<int> AddAsync(TEntity entity)
        {
            MarkAllUnchanged();
            Entities.Add(entity);

            var entry = _context.Entry(entity);
            entry.State = EntityState.Added;

            await _context.SaveChangesAsync();

            return entity.Id;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual async Task DeleteAsync(TEntity entity)
        {
            MarkAllUnchanged();
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;

            Entities.Remove(entity);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            MarkAllUnchanged();
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        private void MarkAllUnchanged()
        {
            var changedEntries = _context.ChangeTracker.Entries();
            foreach (var dbEntityEntry in changedEntries)
            {
                dbEntityEntry.State = EntityState.Unchanged;
            }
        }
    }
}