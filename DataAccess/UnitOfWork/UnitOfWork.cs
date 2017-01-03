using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess.UnitOfWork
{
    /// <summary>
    /// The unit of work.
    /// </summary>
    /// <seealso cref="IUnitOfWorkManager" />
    /// <seealso cref="IUnitOfWork" />
    internal class UnitOfWork : IUnitOfWorkManager, IUnitOfWork
    {
        private readonly IUnitOfWorkProcessor _unitOfWorkProcessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="unitOfWorkProcessor">The unit of work processor.</param>
        public UnitOfWork(IUnitOfWorkProcessor unitOfWorkProcessor)
        {
            _unitOfWorkProcessor = unitOfWorkProcessor;
            Entities = new List<UnitOfWorkEntity>();
        }

        /// <summary>
        /// Gets the list of unit of work entities.
        /// </summary>
        public IList<UnitOfWorkEntity> Entities { get; }

        /// <summary>
        /// Marks the entity as new.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MarkAsNew(Entity entity)
        {
            RemoveIfExist(entity);
            Entities.Add(new UnitOfWorkEntity(entity, UnitOfWorkState.New));
        }

        /// <summary>
        /// Marks the entity as updated.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MarkAsUpdated(Entity entity)
        {
            RemoveIfExist(entity);
            Entities.Add(new UnitOfWorkEntity(entity, UnitOfWorkState.Updated));
        }

        /// <summary>
        /// Marks the entity as deleted.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void MarkAsDeleted(Entity entity)
        {
            RemoveIfExist(entity);
            Entities.Add(new UnitOfWorkEntity(entity, UnitOfWorkState.Deleted));
        }

        private void RemoveIfExist(Entity entity)
        {
            if (Entities.Any(it => it.Entity == entity))
            {
                Entities.Remove(Entities.Single(it => it.Entity == entity));
            }
        }

        /// <summary>
        /// Saves the changes of the entities.
        /// </summary>
        public async Task SaveChanges()
        {
            await _unitOfWorkProcessor.Process(Entities);
        }
    }
}