using System.Web;
using System.Web.Http;
using TemplateProject.WebAPI.AppStart;

namespace TemplateProject.WebAPI
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
            GlobalConfiguration.Configure(RouteConfiguration.RegisterRoutes);
            GlobalConfiguration.Configure(FormattersConfiguration.RegisterFormatters);
            GlobalConfiguration.Configure(DependencyResolverConfiguration.RegisterResolver);
        }
    }
}