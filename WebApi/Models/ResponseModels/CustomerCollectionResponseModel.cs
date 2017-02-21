using System.Collections.Generic;
using TemplateProject.WebApi.Models.ResponseModels;

namespace TemplateProject.WebAPI.Models.ResponseModels
{
    /// <summary>
    /// Represents the collection of the customers.
    /// </summary>
    /// <seealso cref="TemplateProject.WebApi.Models.ResponseModels.ModelWithLinks" />
    public class CustomerCollectionResponseModel : ModelWithLinks
    {
        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        public IList<CustomerResponseModel> Customers { get; set; }
    }
}