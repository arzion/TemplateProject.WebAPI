﻿using System.Web.Http;
using TemplateProject.WebAPI.Infrastracture.Routes;

namespace TemplateProject.WebAPI.AppStart
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
                "CustomerSearch", "customer/search/{keyword}", new { controller = "Customer", Action = "GetByName" });
            config.Routes.MapRouteWithName(
                "Customer", "customer/{id}", new { controller = "Customer", id = RouteParameter.Optional });
        }
    }
}