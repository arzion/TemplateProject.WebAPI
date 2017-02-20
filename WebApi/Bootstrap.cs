using System.Web.Http;
using TemplateProject.WebApi.AppStart;

namespace TemplateProject.WebApi
{
    /// <summary>
    /// The bootstrapper of the Application. Builds all dependency that are needed by application on its lifestyle.
    /// </summary>
    public static class Bootstrap
    {
        /// <summary>
        /// Configures the application with its dependencies.
        /// </summary>
        /// <param name="configuration">The application http configuration.</param>
        public static void Configure(HttpConfiguration configuration)
        {
            DependencyResolverConfiguration.RegisterResolver(configuration);
            FormattersConfiguration.RegisterFormatters(configuration);
            RouteConfiguration.RegisterRoutes(configuration);
        }
    }
}