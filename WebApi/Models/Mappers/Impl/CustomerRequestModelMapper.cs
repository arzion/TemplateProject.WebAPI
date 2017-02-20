using System;
using TemplateProject.DomainModel;
using TemplateProject.DomainModel.Factories;
using TemplateProject.WebApi.Models.RequestModels;

namespace TemplateProject.WebApi.Models.Mappers.Impl
{
    /// <summary>
    /// The mapper of the request model to customer.
    /// </summary>
    public class CustomerRequestModelMapper : ICustomerRequestModelMapper
    {
        private readonly ICustomerFactory _customerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRequestModelMapper"/> class.
        /// </summary>
        /// <param name="customerFactory">The customer factory.</param>
        public CustomerRequestModelMapper(ICustomerFactory customerFactory)
        {
            _customerFactory = customerFactory;
        }

        /// <summary>
        /// Maps the request model to customer.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>
        /// Mapped customer.
        /// </returns>
        public Customer MapToCustomer(CustomerRequestModel data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            return _customerFactory.Create(data.FirstName, data.LastName);
        }

        /// <summary>
        /// Merges the request model to customer.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="customer">The customer.</param>
        public void MergeToCustomer(CustomerRequestModel data, Customer customer)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            customer.FirstName = string.IsNullOrEmpty(data.FirstName)
                ? customer.FirstName
                : data.FirstName;
            customer.LastName = string.IsNullOrEmpty(data.LastName)
                ? customer.LastName
                : data.LastName;
        }
    }
}