using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace TemplateProject.WebAPI.Infrastracture.Formatters
{
    /// <summary>
    /// Case sensitive JsonMediaTypeFormatter.
    /// </summary>
    public class CustomJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomJsonMediaTypeFormatter"/> class.
        /// </summary>
        public CustomJsonMediaTypeFormatter()
        {
            SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedJsonMediaTypeFormatter"/> class for specific type of the media.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        public CustomJsonMediaTypeFormatter(MediaTypeHeaderValue mediaType) : this()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(mediaType);
        }
    }
}