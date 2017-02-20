using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Reflection;

namespace TemplateProject.WebAPI.Infrastracture.Formatters
{
    /// <summary>
    /// Provider of supported media types of the application.
    /// </summary>
    public static class MediaTypeFormattersProvider
    {
        private static readonly IList<JsonMediaTypeFormatter> TypedJsonMediaTypeFormatters = new List<JsonMediaTypeFormatter>();
        private static readonly IList<MediaTypeHeaderValue> XmlMediaTypeHeaderValues = new List<MediaTypeHeaderValue>();

        /// <summary>
        /// Gets the formatters of the application.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// The list of available formatters of the application.
        /// </returns>
        public static IEnumerable<MediaTypeFormatter> GetFormatters(Assembly assembly)
        {
            var modelTypes = GetModelTypes(assembly);

            var xmlMediaTypeHeaderValues = new List<MediaTypeHeaderValue>();

            foreach (var modelType in modelTypes)
            {
                var mediaTypes = modelType
                    .GetCustomAttributes<MediaTypeAttribute>(true)
                    .Select(mediaTypeAttribute => mediaTypeAttribute.Name).ToArray();

                if (mediaTypes.Any())
                {
                    foreach (var mediaType in mediaTypes)
                    {
                        xmlMediaTypeHeaderValues.Add(new MediaTypeHeaderValue($"{mediaType}+xml"));

                        yield return new TypedJsonMediaTypeFormatter(modelType, new MediaTypeHeaderValue($"{mediaType}+json"));
                    }
                }
                else
                {
                    var mediaType = ToMediaType(modelType);

                    xmlMediaTypeHeaderValues.Add(new MediaTypeHeaderValue($"{mediaType}+xml"));

                    yield return new TypedJsonMediaTypeFormatter(modelType, new MediaTypeHeaderValue($"{mediaType}+json"));
                }
            }

            foreach (var jsonMediaTypeFormatters in TypedJsonMediaTypeFormatters)
            {
                yield return jsonMediaTypeFormatters;
            }

            xmlMediaTypeHeaderValues.AddRange(XmlMediaTypeHeaderValues);

            yield return new XmlMediaTypeFormatter(xmlMediaTypeHeaderValues.ToArray());

            yield return new CustomJsonMediaTypeFormatter();
        }

        /// <summary>
        /// Registers the json media type.
        /// </summary>
        /// <param name="mediaType">Type of the media.</param>
        public static void RegisterJsonMediaTypeFormatter(string mediaType)
        {
            TypedJsonMediaTypeFormatters.Add(
                new CustomJsonMediaTypeFormatter(new MediaTypeHeaderValue($"{mediaType}+json")));
        }
        
        /// <summary>
        /// Registers the typed json media type formatter.
        /// </summary>
        /// <typeparam name="T">The type for which the Typed formatter should be registered.</typeparam>
        /// <param name="mediaType">The media type.</param>
        public static void RegisterTypedJsonMediaTypeFormatter<T>(string mediaType)
        {
            RegisterTypedJsonMediaTypeFormatter(typeof(T), mediaType);
        }

        /// <summary>
        /// Registers the typed json media type formatter.
        /// </summary>
        /// <param name="modelType">The type for which the Typed formatter should be registered.</param>
        /// <param name="mediaType">The media type.</param>
        public static void RegisterTypedJsonMediaTypeFormatter(Type modelType, string mediaType)
        {
            TypedJsonMediaTypeFormatters.Add(
                new TypedJsonMediaTypeFormatter(modelType, new MediaTypeHeaderValue($"{mediaType}+json")));
        }

        /// <summary>
        /// Registers the xml media type.
        /// </summary>
        /// <param name="mediaType">The xml media type.</param>
        public static void RegisterXmlMediaType(string mediaType)
        {
            XmlMediaTypeHeaderValues.Add(new MediaTypeHeaderValue($"{mediaType}+xml"));
        }

        /// <summary>
        /// Registers both json and xml the type of the media.
        /// </summary>
        /// <param name="mediaType">The media type to register.</param>
        public static void RegisterMediaType(string mediaType)
        {
            RegisterJsonMediaTypeFormatter(mediaType);
            RegisterXmlMediaType(mediaType);
        }

        /// <summary>
        /// Get the media type of the model.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="version">The version.</param>
        /// <returns>The built model media type.</returns>
        public static string ToMediaType<T>(string version = "")
        {
            return ToMediaType(typeof(T), version);
        }

        /// <summary>
        /// Get the media type of the model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <param name="version">The version.</param>
        /// <returns>The built model media type.</returns>
        /// <exception cref="System.ArgumentException">Type should end with prefix RequestModel, ResponseModel or Model</exception>
        public static string ToMediaType(Type modelType, string version = "")
        {
            var modelName = modelType.Name;
            var indexToRemovePrefix = modelName.LastIndexOf("RequestModel", StringComparison.Ordinal);
            if (indexToRemovePrefix == -1)
            {
                indexToRemovePrefix = modelName.LastIndexOf("ResponseModel", StringComparison.Ordinal);
            }
            if (indexToRemovePrefix == -1)
            {
                indexToRemovePrefix = modelName.LastIndexOf("Model", StringComparison.Ordinal);
            }
            if (indexToRemovePrefix == -1)
            {
                throw new ArgumentException("Type should end with prefix RequestModel, ResponseModel or Model");
            }

            var modelNameInMediaType = modelName
                .Remove(indexToRemovePrefix)
                .Aggregate(string.Empty, (s, c) => s + (char.IsUpper(c) && s.Any() ? "-" : string.Empty) + c)
                .ToLowerInvariant();

            var versionPrefix = string.IsNullOrEmpty(version) ? string.Empty : $".v{version}";

            return $"application/quotemycad.{modelNameInMediaType}{versionPrefix}";
        }

        private static IEnumerable<Type> GetModelTypes(Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(t => t.Name.EndsWith("Model"));
        }
    }
}