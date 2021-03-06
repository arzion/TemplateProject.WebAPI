﻿using System.Threading.Tasks;
using System.Web.Http;
using TemplateProject.DataAccess;
using TemplateProject.DomainModel;
using TemplateProject.WebApi.Models.Mappers;
using TemplateProject.WebApi.Models.RequestModels;

namespace TemplateProject.WebApi.Controllers
{
    /// <summary>
    /// The controller that works with customer entity.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CustomerController : ApiController
    {
        private readonly IReader<Customer> _customerReader;
        private readonly ITransactionRunner _transactionRunner;

        private readonly ICustomerMapper _customerMapper;
        private readonly ICustomerRequestModelMapper _customerRequestModelMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController" /> class.
        /// </summary>
        /// <param name="customerReader">The customer reader.</param>
        /// <param name="customerMapper">The customer mapper.</param>
        /// <param name="customerRequestModelMapper">The customer request model mapper.</param>
        /// <param name="transactionRunner">The transaction runner.</param>
        public CustomerController(
            IReader<Customer> customerReader,
            ICustomerMapper customerMapper,
            ICustomerRequestModelMapper customerRequestModelMapper,
            ITransactionRunner transactionRunner)
        {
            _customerReader = customerReader;
            _customerMapper = customerMapper;
            _customerRequestModelMapper = customerRequestModelMapper;
            _transactionRunner = transactionRunner;
        }

        /// <summary>
        /// Gets the customer by specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The found customer.</returns>
        public async Task<IHttpActionResult> Get(int id)
        {
            var customer = await _customerReader.FindAsync(id);

            return customer == null
                ? NotFound()
                : Ok(_customerMapper.MapToResponseModel(customer)) as IHttpActionResult;
        }

        /// <summary>
        /// Updates the customer by first and last name.
        /// </summary>
        /// <param name="id">The identifier of the customer.</param>
        /// <param name="data">The data to update customer fields.</param>
        /// <returns>
        /// Updated customer.
        /// </returns>
        public async Task<IHttpActionResult> Put(int id, [FromBody]CustomerUpdateRequestModel data)
        {
            var customer = await _customerReader.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            _customerRequestModelMapper.MergeToCustomer(data, customer);

            await _transactionRunner.Run(unitOfWork => unitOfWork.MarkAsUpdated(customer));

            return Ok();
        }

        /// <summary>
        /// Creates the customer by first and last name.
        /// </summary>
        /// <param name="id">The identifier of the customer.</param>
        /// <returns>
        /// Result of Delete operation.
        /// </returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            var customer = await _customerReader.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            await _transactionRunner.Run(unitOfWork => unitOfWork.MarkAsDeleted(customer));

            return Ok();
        }
    }
}