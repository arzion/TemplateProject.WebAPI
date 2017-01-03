using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess.UnitOfWork
{
    /// <summary>
    /// The manager of Unit Of Work that allows to set entity to particular state.
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// Marks the entity as new.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void MarkAsNew(Entity entity);

        /// <summary>
        /// Marks the entity as updated.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void MarkAsUpdated(Entity entity);

        /// <summary>
        /// Marks the entity as deleted.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void MarkAsDeleted(Entity entity);
    }
}