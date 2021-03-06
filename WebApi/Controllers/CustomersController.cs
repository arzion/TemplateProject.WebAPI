﻿using System.Threading.Tasks;
using System.Web.Http;
using TemplateProject.DataAccess;
using TemplateProject.DomainModel;
using TemplateProject.WebApi.Models.Mappers;
using TemplateProject.WebApi.Models.RequestModels;
using TemplateProject.WebApi.Utils;

namespace TemplateProject.WebApi.Controllers
{
    /// <summary>
    /// The controller that works with customer collection.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class CustomersController : ApiController
    {
        private readonly IReader<Customer> _customerReader;
        private readonly ITransactionRunner _transactionRunner;

        private readonly ICustomerMapper _customerMapper;
        private readonly ICustomerRequestModelMapper _customerRequestModelMapper;
        private readonly IUrlHelper _urlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerController" /> class.
        /// </summary>
        /// <param name="customerReader">The customer reader.</param>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="customerMapper">The customer mapper.</param>
        /// <param name="customerRequestModelMapper">The customer request model mapper.</param>
        /// <param name="transactionRunner">The transaction runner.</param>
        public CustomersController(
            IReader<Customer> customerReader,
            IUrlHelper urlHelper,
            ICustomerMapper customerMapper,
            ICustomerRequestModelMapper customerRequestModelMapper,
            ITransactionRunner transactionRunner)
        {
            _customerReader = customerReader;
            _urlHelper = urlHelper;
            _customerMapper = customerMapper;
            _customerRequestModelMapper = customerRequestModelMapper;
            _transactionRunner = transactionRunner;
        }

        /// <summary>
        /// Gets the customer by specified identifier.
        /// </summary>
        /// <returns>All found customers.</returns>
        public async Task<IHttpActionResult> GetAll()
        {
            var customers = await _customerReader.FindAllAsync();

            var responseModel = _customerMapper.MapToResponseModel(customers);
            return Ok(responseModel);
        }

        /// <summary>
        /// Gets the customer by name keyword.
        /// </summary>
        /// <param name="keyword">The keyword to search.</param>
        /// <returns>The found customers by keyword.</returns>
        public async Task<IHttpActionResult> GetByName(string keyword)
        {
            var customers = await _customerReader.FindByCriteriaAsync(
                it => it.FirstName.Contains(keyword)
                      || it.LastName.Contains(keyword));

            var responseModel = _customerMapper.MapToResponseModel(customers);
            return Ok(responseModel);
        }

        /// <summary>
        /// Creates the customer by first and last name.
        /// </summary>
        /// <param name="data">The data to create customer.</param>
        /// <returns>
        /// Created customer identifier.
        /// </returns>
        public async Task<IHttpActionResult> Post([FromBody]CustomerCreateRequestModel data)
        {
            var customer = _customerRequestModelMapper.MapToCustomer(data);
            await _transactionRunner.Run(unitOfWork => unitOfWork.MarkAsNew(customer));
            var id = customer.Id;

            return Created(
                _urlHelper.GetUri<CustomerController>(c => c.Get(id)).AbsoluteUri,
                id);
        }
    }
}