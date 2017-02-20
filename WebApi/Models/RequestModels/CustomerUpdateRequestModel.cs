namespace TemplateProject.WebApi.Models.RequestModels
{
    /// <summary>
    /// The request model that is used for customer update.
    /// </summary>
    public class CustomerUpdateRequestModel
    {
        /// <summary>
        /// Gets or sets the first name of customer to update.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of customer to update.
        /// </summary>
        public string LastName { get; set; }
    }
}