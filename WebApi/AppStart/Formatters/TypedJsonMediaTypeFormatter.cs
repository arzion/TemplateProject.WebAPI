using System;
using System.Net.Http.Headers;

namespace TemplateProject.WebAPI.AppStart.Formatters
{
    /// <summary>
    /// Case sensitive and typed JsonMediaTypeFormatter
    /// </summary>
    public class TypedJsonMediaTypeFormatter : CustomJsonMediaTypeFormatter
    {
        private readonly Type _resourceType;

        public TypedJsonMediaTypeFormatter(Type resourceType, MediaTypeHeaderValue mediaType) : base(mediaType)
        {
            _resourceType = resourceType;
        }

        public override bool CanReadType(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return _resourceType == type;
        }

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