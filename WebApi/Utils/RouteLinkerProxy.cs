using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ploeh.Hyprlinkr;

namespace TemplateProject.WebApi.Utils
{
    /// <summary>
    /// <see cref="RouteLinker"/> proxy.
    /// </summary>
    public class RouteLinkerProxy : IUrlHelper
    {
        private readonly RouteLinker _routeLinker;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteLinkerProxy" /> class.
        /// </summary>
        /// <param name="routeLinker">The route helper.</param>
        public RouteLinkerProxy(RouteLinker routeLinker)
        {
            _routeLinker = routeLinker;
        }

        /// <summary>
        /// Strongly typed fully qualified URL generation to an action method by specifying controller type and controller action with parameters.
        /// </summary>
        /// <typeparam name="T">Controller type.</typeparam>
        /// <param name="action">Controller action expression with controller parameters.</param>
        /// <returns> The fully qualified URL to an action method.</returns>
        public Uri GetUri<T>(Expression<Action<T>> action)
        {
            return _routeLinker.GetUri(action);
        }

        /// <summary>
        /// Strongly typed fully qualified URL generation to an action method by specifying controller type and controller action with parameters.
        /// </summary>
        /// <typeparam name="T">Controller type.</typeparam>
        /// <param name="action">Controller action expression with controller parameters.</param>
        /// <returns> The fully qualified URL to an action method.</returns>
        public Uri GetUri<T>(Expression<Func<T, Task>> action)
        {
            return _routeLinker.GetUri(action);
        }
    }
}