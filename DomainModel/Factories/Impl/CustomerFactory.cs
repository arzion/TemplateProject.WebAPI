namespace TemplateProject.DomainModel.Factories.Impl
{
    /// <summary>
    /// The factory that creates the customer.
    /// </summary>
    public class CustomerFactory : ICustomerFactory
    {
        /// <summary>
        /// Creates the customer by specified fields.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns>Created customer.</returns>
        public Customer Create(string firstName, string lastName)
        {
            return new Customer
            {
                FirstName = firstName,
                LastName = lastName
            };
        }
    }
}