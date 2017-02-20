using System.Web.Http;
using TemplateProject.WebApi.Infrastracture.Routes;

namespace TemplateProject.WebApi.AppStart
{
    /// <summary>
    /// Configuration of the application routes.
    /// </summary>
    public static class RouteConfiguration
    {
        /// <summary>
        /// Registers the routes of the application.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void RegisterRoutes(HttpConfiguration config)
        {
            config.Routes.MapRouteWithName("DefaultApi", "", new { controller = "Home" });

            // Customer
            config.Routes.MapRouteWithName(
                "CustomerSearch", "customers/search/{keyword}", new { controller = "Customers", Action = "GetByName" });
            config.Routes.MapRouteWithName(
                "Customers", "customers", new { controller = "Customers" });
            config.Routes.MapRouteWithName(
                "Customer", "customer/{id}", new { controller = "Customer" });
        }
    }
}