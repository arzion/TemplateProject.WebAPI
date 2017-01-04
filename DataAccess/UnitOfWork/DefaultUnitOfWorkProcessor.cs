using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateProject.DataAccess.UnitOfWork
{
    /// <summary>
    /// The processor of the Unit of Work entities.
    /// </summary>
    public class DefaultUnitOfWorkProcessor : IUnitOfWorkProcessor
    {
        /// <summary>
        /// Process the changes of the entities.
        /// </summary>
        public virtual async Task Process(IEnumerable<UnitOfWorkEntity> entities)
        {
            var entitiesToProcess = entities.ToList();
            foreach (var addedEntities in entitiesToProcess.Where(it => it.State == UnitOfWorkState.New))
            {
                var writer = Configuration.WriterFactory(addedEntities.Entity);
                await ((dynamic)writer).AddAsync((dynamic)addedEntities.Entity);
            }

            foreach (var updatedEntities in entitiesToProcess.Where(it => it.State == UnitOfWorkState.Updated))
            {
                var writer = Configuration.WriterFactory(updatedEntities.Entity);
                await ((dynamic)writer).UpdateAsync((dynamic)updatedEntities.Entity);
            }

            foreach (var deletedEntities in entitiesToProcess.Where(it => it.State == UnitOfWorkState.Deleted))
            {
                var writer = Configuration.WriterFactory(deletedEntities.Entity);
                await ((dynamic)writer).DeleteAsync((dynamic)deletedEntities.Entity);
            }
        }
    }
}