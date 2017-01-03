using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess.UnitOfWork
{
    /// <summary>
    /// The state of the entity in unit of work.
    /// </summary>
    public class UnitOfWorkEntity
    {
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; }

        /// <summary>
        /// Gets the state of the entity.
        /// </summary>
        public UnitOfWorkState State { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkEntity"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="state">The state.</param>
        public UnitOfWorkEntity(Entity entity, UnitOfWorkState state)
        {
            Entity = entity;
            State = state;
        }
    }
}