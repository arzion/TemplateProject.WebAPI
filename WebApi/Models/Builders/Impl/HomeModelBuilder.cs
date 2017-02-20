using System.Collections.Generic;
using TemplateProject.WebApi.Models.LinksFactories;
using TemplateProject.WebApi.Models.ResponseModels;

namespace TemplateProject.WebApi.Models.Builders.Impl
{
    /// <summary>
    /// The builder of the <see cref="HomeResponseModel"/>.
    /// </summary>
    public class HomeModelBuilder : IHomeModelBuilder
    {
        private readonly ICustomerLinksFactory _customerLinksFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeModelBuilder" /> class.
        /// </summary>
        /// <param name="customerLinksFactory">The builder of the links.</param>
        public HomeModelBuilder(ICustomerLinksFactory customerLinksFactory)
        {
            _customerLinksFactory = customerLinksFactory;
        }

        /// <summary>
        /// Builds the <see cref="HomeResponseModel"/>.
        /// </summary>
        /// <returns>Built model for Home Controller.</returns>
        public HomeResponseModel Build()
        {
            return new HomeResponseModel
            {
                Links = new List<LinkModel>
                {
                    _customerLinksFactory.CustomerTemplate,
                    _customerLinksFactory.CustomerSearchTemplate
                }
            };
        }
    }
}