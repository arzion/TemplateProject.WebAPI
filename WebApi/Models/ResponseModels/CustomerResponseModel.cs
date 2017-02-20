namespace TemplateProject.WebApi.Models.ResponseModels
{
    /// <summary>
    /// The customer model.
    /// </summary>
    /// <seealso cref="ModelWithLinks" />
    public class CustomerResponseModel : ModelWithLinks
    {
        /// <summary>
        /// Gets or sets the identifier of the Customer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        public string LastName { get; set; }
    }
}