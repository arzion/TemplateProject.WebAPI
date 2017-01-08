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
        private readonly IWriterFactory _writerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultUnitOfWorkProcessor"/> class.
        /// </summary>
        /// <param name="writerFactory">The writer factory.</param>
        public DefaultUnitOfWorkProcessor(IWriterFactory writerFactory)
        {
            _writerFactory = writerFactory;
        }

        /// <summary>
        /// Process the changes of the entities.
        /// </summary>
        public virtual async Task Process(IEnumerable<UnitOfWorkEntity> entities)
        {
            var entitiesToProcess = entities.ToList();
            foreach (var addedEntities in entitiesToProcess.Where(it => it.State == UnitOfWorkState.New))
            {
                var writer = _writerFactory.Create(addedEntities.Entity);
                await ((dynamic)writer).AddAsync((dynamic)addedEntities.Entity);
            }

            foreach (var updatedEntities in entitiesToProcess.Where(it => it.State == UnitOfWorkState.Updated))
            {
                var writer = _writerFactory.Create(updatedEntities.Entity);
                await ((dynamic)writer).UpdateAsync((dynamic)updatedEntities.Entity);
            }

            foreach (var deletedEntities in entitiesToProcess.Where(it => it.State == UnitOfWorkState.Deleted))
            {
                var writer = _writerFactory.Create(deletedEntities.Entity);
                await ((dynamic)writer).DeleteAsync((dynamic)deletedEntities.Entity);
            }
        }
    }
}