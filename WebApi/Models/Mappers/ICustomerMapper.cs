using System.Collections.Generic;
using TemplateProject.DomainModel;
using TemplateProject.WebAPI.Models.ResponseModels;

namespace TemplateProject.WebAPI.Models.Mappers
{
    /// <summary>
    /// The mapper of <see cref="Customer"/> to contracts.
    /// </summary>
    public interface ICustomerMapper
    {
        /// <summary>
        /// Maps to response model.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns>Mapped response model.</returns>
        CustomerResponseModel MapToResponseModel(Customer customer);

        /// <summary>
        /// Maps to collection of response models.
        /// </summary>
        /// <param name="customers">The customers to mapped.</param>
        /// <returns>Mapped collection of customer model.</returns>
        IEnumerable<CustomerResponseModel> MapToResponseModel(IEnumerable<Customer> customers);
    }
}