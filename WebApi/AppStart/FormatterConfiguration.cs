using System.Web.Http;
using TemplateProject.WebAPI.Infrastracture.Formatters;

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
            config.Formatters.Clear();

            // Register all models according to convensions from this assembly
            config.Formatters.AddRange(MediaTypeFormattersProvider.GetFormatters(typeof(WebApiApplication).Assembly));
        }
    }
}