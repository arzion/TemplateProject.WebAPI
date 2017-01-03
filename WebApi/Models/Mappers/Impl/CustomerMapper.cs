using System.Collections.Generic;
using System.Linq;
using TemplateProject.DomainModel;
using TemplateProject.WebAPI.Models.LinksFactories;
using TemplateProject.WebAPI.Models.ResponseModels;

namespace TemplateProject.WebAPI.Models.Mappers.Impl
{
    /// <summary>
    /// The mapper of <see cref="Customer"/> to contracts.
    /// </summary>
    public class CustomerMapper : ICustomerMapper
    {
        private readonly ICustomerLinksFactory _customerLinksFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerMapper"/> class.
        /// </summary>
        /// <param name="customerLinksFactory">The customer links factory.</param>
        public CustomerMapper(ICustomerLinksFactory customerLinksFactory)
        {
            _customerLinksFactory = customerLinksFactory;
        }

        /// <summary>
        /// Maps to response model.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <returns>Mapped response model.</returns>
        public CustomerResponseModel MapToResponseModel(Customer customer)
        {
            var model = new CustomerResponseModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Id = customer.Id
            };
            model.Links.Add(_customerLinksFactory.CustomerLink(customer.Id));
            return model;
        }

        /// <summary>
        /// Maps to collection of response models.
        /// </summary>
        /// <param name="customers">The customers to mapped.</param>
        /// <returns>Mapped collection of customer model.</returns>
        public IEnumerable<CustomerResponseModel> MapToResponseModel(IEnumerable<Customer> customers)
        {
            return customers.Select(MapToResponseModel);
        }
    }
}