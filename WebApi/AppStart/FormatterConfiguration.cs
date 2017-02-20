using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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
            var jsonFormatter = new JsonMediaTypeFormatter
            {
                SerializerSettings =
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            };
            jsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });

            config.Formatters.Clear();

            config.Formatters.Add(jsonFormatter);
        }
    }
}