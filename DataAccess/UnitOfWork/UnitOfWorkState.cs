namespace TemplateProject.DataAccess.UnitOfWork
{
    /// <summary>
    /// The state of the entity in unit of work.
    /// </summary>
    public enum UnitOfWorkState
    {
        /// <summary>
        /// The new entity.
        /// </summary>
        New,

        /// <summary>
        /// The updated entity.
        /// </summary>
        Updated,

        /// <summary>
        /// The deleted entity.
        /// </summary>
        Deleted
    }
}