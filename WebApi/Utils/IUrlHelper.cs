using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TemplateProject.WebAPI.Utils
{
    /// <summary>
    /// Allows to get strongly typed action urls.
    /// </summary>
    public interface IUrlHelper
    {
        /// <summary>
        /// Generates a fully qualified URL to strongly typed action method by using the controller type and 
        /// </summary>
        /// <typeparam name="T">Controller type</typeparam>
        /// <param name="action">Action call with specified parameters which will be used as route values</param>
        /// <returns>The fully qualified URI to an action method.</returns>
        Uri GetUri<T>(Expression<Action<T>> action);

        /// <summary>
        /// Generates a fully qualified URL to strongly typed action method by using the controller type and 
        /// </summary>
        /// <typeparam name="T">Controller type</typeparam>
        /// <param name="action">Action call with specified parameters which will be used as route values</param>
        /// <returns>The fully qualified URI to an action method.</returns>
        Uri GetUri<T>(Expression<Func<T, Task>> action);
    }
}