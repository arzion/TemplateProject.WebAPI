using System;
using System.Net.Http.Headers;

namespace TemplateProject.WebAPI.Infrastracture.Formatters
{
    /// <summary>
    /// Case sensitive and typed JsonMediaTypeFormatter.
    /// </summary>
    public class TypedJsonMediaTypeFormatter : CustomJsonMediaTypeFormatter
    {
        private readonly Type _resourceType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedJsonMediaTypeFormatter"/> class.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="mediaType">Type of the media.</param>
        public TypedJsonMediaTypeFormatter(Type resourceType, MediaTypeHeaderValue mediaType) : base(mediaType)
        {
            _resourceType = resourceType;
        }

        /// <summary>
        /// Determines whether this <see cref="T:System.Net.Http.Formatting.JsonMediaTypeFormatter" /> can read objects of the specified <paramref name="type" />.
        /// </summary>
        /// <param name="type">The type of object that will be read.</param>
        /// <returns>
        /// true if objects of this <paramref name="type" /> can be read, otherwise false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public override bool CanReadType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return _resourceType == type;
        }

        /// <summary>
        /// Determines whether this <see cref="T:System.Net.Http.Formatting.JsonMediaTypeFormatter" /> can write objects of the specified <paramref name="type" />.
        /// </summary>
        /// <param name="type">The type of object that will be written.</param>
        /// <returns>
        /// true if objects of this <paramref name="type" /> can be written, otherwise false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public override bool CanWriteType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return _resourceType == type;
        }
    }
}