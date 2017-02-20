using System.Net;
using System.Net.Http;
using System.Web.Http;
using TemplateProject.WebApi.Models.Builders;

namespace TemplateProject.WebApi.Controllers
{
    /// <summary>
    /// The main controller of the application.
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class HomeController : ApiController
    {
        private readonly IHomeModelBuilder _homeModelBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="homeModelBuilder">The builder.</param>
        public HomeController(IHomeModelBuilder homeModelBuilder)
        {
            _homeModelBuilder = homeModelBuilder;
        }

        /// <summary>
        /// Gets the list of the application related resources.
        /// </summary>
        /// <returns>The list of the application home resources.</returns>
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _homeModelBuilder.Build());
        }
    }
}