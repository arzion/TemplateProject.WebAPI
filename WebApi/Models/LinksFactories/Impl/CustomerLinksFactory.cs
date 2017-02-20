using TemplateProject.WebApi.Controllers;
using TemplateProject.WebApi.Infrastracture;
using TemplateProject.WebApi.Models.ResponseModels;
using TemplateProject.WebApi.Utils;

namespace TemplateProject.WebApi.Models.LinksFactories.Impl
{
    /// <summary>
    /// Builder of the link models for customer resource.
    /// </summary>
    public class CustomerLinksFactory : ICustomerLinksFactory
    {
        private readonly IUrlHelper _urlHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerLinksFactory" /> class.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        public CustomerLinksFactory(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        /// <summary>
        /// Gets the customer link model.
        /// </summary>
        public LinkModel CustomerTemplate
        {
            get
            {
                const int id = int.MaxValue;

                var template = _urlHelper.GetUri<CustomerController>(c => c.Get(id)).AbsoluteUri;
                template = template
                    .Replace(id.ToString(), "{id}");

                return new LinkModel(template, Rels.Customer);
            }
        }

        /// <summary>
        /// Gets the customer link model.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Built link model for particular customer.</returns>
        public LinkModel CustomerLink(int id)
        {
            return new LinkModel(
                _urlHelper.GetUri<CustomerController>(c => c.Get(id)).AbsoluteUri,
                Rels.Customer);
        }

        /// <summary>
        /// Gets the customer link model.
        /// </summary>
        public LinkModel CustomerSearchTemplate
        {
            get
            {
                var keyword = int.MaxValue.ToString();

                var template = _urlHelper.GetUri<CustomersController>(c => c.GetByName(keyword)).AbsoluteUri;
                template = template
                    .Replace(keyword, "{keyword}");

                return new LinkModel(template, Rels.CustomerSearch);
            }
        }
    }
}