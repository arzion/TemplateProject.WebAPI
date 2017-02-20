using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ploeh.Hyprlinkr;

namespace TemplateProject.WebApi.Infrastracture.Routes
{
    /// <summary>
    /// The dispatcher of the application routes. Can be used to build strong-typed links.
    /// </summary>
    /// <seealso cref="Ploeh.Hyprlinkr.IRouteDispatcher" />
    public class HttpRouteCollectionDispatcher : IRouteDispatcher
    {
        private readonly IRouteDispatcher _defaultDispatcher;
        private static readonly IList<RouteDefinition> RouteDefinitions = new List<RouteDefinition>();

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRouteCollectionDispatcher"/> class.
        /// </summary>
        public HttpRouteCollectionDispatcher()
        {
            _defaultDispatcher = new DefaultRouteDispatcher("DefaultApi");
        }

        /// <summary>
        /// Provides dispatch information based on an Action Method.
        /// </summary>
        /// <param name="method">The method expression.</param>
        /// <param name="routeValues">Route values.</param>
        /// <returns>
        /// An object containing the route name, as well as the route values.
        /// </returns>
        /// <remarks>
        /// Note to implementers: Pass <paramref name="routeValues" /> through
        /// to the return value if you don't modify it. However, if you wish to
        /// add or remove values from the dictionary, you should create a copy
        /// and mutate that copy, leaving the input unmodified.
        /// </remarks>
        public Rouple Dispatch(MethodCallExpression method, IDictionary<string, object> routeValues)
        {
            var controller = method.Method.ReflectedType?.Name.Replace("Controller", string.Empty);
            var action = method.Method.Name;

            var foundRouple = RouteDefinitions
                .FirstOrDefault(r => !string.IsNullOrEmpty(r.Action)
                    ? r.Controller == controller && r.Action == action
                    : r.Controller == controller);

            return foundRouple != null
                ? new Rouple(foundRouple.RouteName, routeValues)
                : _defaultDispatcher.Dispatch(method, routeValues);
        }

        /// <summary>
        /// Registers the rouple that can be used during dispathing.
        /// </summary>
        /// <param name="routeName">Name of the route.</param>
        /// <param name="controller">The controller name.</param>
        /// <param name="action">The action name.</param>
        public static void RegisterRouple(string routeName, string controller, string action)
        {
            RouteDefinitions.Add(new RouteDefinition
            {
                Action = action,
                Controller = controller,
                RouteName = routeName
            });
        }

        private class RouteDefinition
        {
            public string RouteName { get; set; }

            public string Controller { get; set; }

            public string Action { get; set; }
        }
    }
}