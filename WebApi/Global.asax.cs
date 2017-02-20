using System.Web;
using System.Web.Http;

namespace TemplateProject.WebApi
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// Executes once on the initializing of the application.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(Bootstrap.Configure);
        }
    }
}