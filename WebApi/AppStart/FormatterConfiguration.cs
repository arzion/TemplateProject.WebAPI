using System.Web.Http;
using TemplateProject.WebAPI.AppStart;

namespace TemplateProject.WebApi.AppStart
{
    /// <summary>
    /// Configuration of the formatters of the application.
    /// </summary>
    public static class FormattersConfiguration
    {
        /// <summary>
        /// Registers the formatters.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void RegisterFormatters(HttpConfiguration config)
        {
            MediaTypeFormattersProvider.OverrideFormatters(
                config,
                typeof(WebApiApplication).Assembly);
        }
    }
}