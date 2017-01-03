namespace TemplateProject.DomainModel
{
    /// <summary>
    /// The Entity of the Domain Model.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets or sets the identifier of the entity.
        /// </summary>
        public virtual int Id { get; set; }
    }
}