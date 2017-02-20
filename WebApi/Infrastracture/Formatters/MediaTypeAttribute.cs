using System;

namespace TemplateProject.WebAPI.Infrastracture.Formatters
{
    /// <summary>
    /// Attribute that can be used to mark the class with custom media type specification.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MediaTypeAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the name of the media-type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaTypeAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the media-type.</param>
        public MediaTypeAttribute(string name)
        {
            Name = name;
        }
    }
}