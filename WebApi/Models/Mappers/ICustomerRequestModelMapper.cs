﻿using TemplateProject.DomainModel;
using TemplateProject.WebApi.Models.RequestModels;

namespace TemplateProject.WebApi.Models.Mappers
{
    public interface ICustomerRequestModelMapper
    {
        /// <summary>
        /// Maps the request model to customer.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Mapped customer.</returns>
        Customer MapToCustomer(CustomerRequestModel data);

        /// <summary>
        /// Merges the request model to customer.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="customer">The customer.</param>
        void MergeToCustomer(CustomerRequestModel data, Customer customer);
    }
}