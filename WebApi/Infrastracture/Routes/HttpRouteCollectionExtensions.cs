using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Routing;

namespace TemplateProject.WebApi.Infrastracture.Routes
{
    /// <summary>
    /// The extentions of the <see cref="HttpRouteCollection"/>.
    /// </summary>
    public static class HttpRouteCollectionExtensions
    {
        /// <summary>
        /// Maps the name of the route with.
        /// </summary>
        /// <param name="routes">The routes.</param>
        /// <param name="name">The name of the route.</param>
        /// <param name="routeTemplate">The route template.</param>
        /// <param name="defaults">The default values of the routes.</param>
        public static void MapRouteWithName(
            this HttpRouteCollection routes,
            string name,
            string routeTemplate,
            object defaults)
        {
            var routeNameDataToken = new Dictionary<string, object>
            {
                {"RouteName", name}
            };
            var defaultValues = new HttpRouteValueDictionary(defaults);
            var controller = defaultValues["controller"].ToString();
            object action;
            defaultValues.TryGetValue("action", out action);

            HttpRouteCollectionDispatcher.RegisterRouple(name, controller, action?.ToString());

            routes.Add(
                name,
                new HttpRoute(
                    routeTemplate,
                    defaultValues,
                    new HttpRouteValueDictionary(null),
                    new HttpRouteValueDictionary(routeNameDataToken)));
        }
    }
}