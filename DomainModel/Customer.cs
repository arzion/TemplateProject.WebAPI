namespace TemplateProject.DomainModel
{
    /// <summary>
    /// The entity that describes the customer.
    /// </summary>
    public class Customer : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Customer"/> class.
        /// </summary>
        protected internal Customer()
        {
        }

        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Gets the full name of the customer.
        /// </summary>
        public virtual string FullName => $"{FirstName} {LastName}";
    }
}