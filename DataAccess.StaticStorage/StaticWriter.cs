using System.Linq;
using System.Threading.Tasks;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess.StaticStorage
{
    /// <summary>
    /// The writer to update the <see cref="Entity" />.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class StaticWriter<TEntity> : IWriter<TEntity> where TEntity : Entity
    {
        public Task<int> AddAsync(TEntity entity)
        {
            var lastId = -1;
            if (Storage<TEntity>.Entities.Any())
            {
                lastId = Storage<TEntity>.Entities.Max(it => it.Id);
            }

            entity.Id = lastId + 1;
            Storage<TEntity>.Entities.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public Task DeleteAsync(TEntity entity)
        {
            var toDelete = Storage<TEntity>.Entities.FirstOrDefault(it => it.Id == entity.Id);
            if (toDelete != null)
            {
                Storage<TEntity>.Entities.Remove(Storage<TEntity>.Entities.Single(it => it.Id == entity.Id));
            }
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TEntity entity)
        {
            var found = Storage<TEntity>.Entities.FirstOrDefault(it => it.Id == entity.Id);
            if (found != null)
            {
                Storage<TEntity>.Entities.Remove(Storage<TEntity>.Entities.Single(it => it.Id == entity.Id));
                Storage<TEntity>.Entities.Add(entity);
            }
            return Task.CompletedTask;
        }
    }
}