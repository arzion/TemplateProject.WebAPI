using System.Collections.Generic;
using TemplateProject.WebAPI.Models.LinksFactories;
using TemplateProject.WebAPI.Models.ResponseModels;

namespace TemplateProject.WebAPI.Models.Builders.Impl
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