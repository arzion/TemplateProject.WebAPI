namespace TemplateProject.DomainModel.Factories
{
    /// <summary>
    /// The factory that creates the customer.
    /// </summary>
    public interface ICustomerFactory
    {
        /// <summary>
        /// Creates the customer by specified fields.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns>Created customer.</returns>
        Customer Create(string firstName, string lastName);
    }
}