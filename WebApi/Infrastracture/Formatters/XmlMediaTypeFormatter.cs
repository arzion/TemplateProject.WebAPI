using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TemplateProject.WebAPI.Infrastracture.Formatters
{
    /// <summary>
    /// The formatter of the xml.
    /// </summary>
    /// <seealso cref="System.Net.Http.Formatting.MediaTypeFormatter" />
    public class XmlMediaTypeFormatter : MediaTypeFormatter
    {
        private readonly UTF8Encoding _encoding = new UTF8Encoding(false, true);

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlMediaTypeFormatter"/> class.
        /// </summary>
        /// <param name="supportedMediaTypes">The supported media types.</param>
        public XmlMediaTypeFormatter(params MediaTypeHeaderValue[] supportedMediaTypes)
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));

            foreach (var mediaTypeHeaderValue in supportedMediaTypes)
            {
                SupportedMediaTypes.Add(mediaTypeHeaderValue);
            }
        }

        /// <summary>
        /// Queries whether this <see cref="T:System.Net.Http.Formatting.MediaTypeFormatter" /> can deserializean object of the specified type.
        /// </summary>
        /// <param name="type">The type to deserialize.</param>
        /// <returns>
        /// true if the <see cref="T:System.Net.Http.Formatting.MediaTypeFormatter" /> can deserialize the type; otherwise, false.
        /// </returns>
        public override bool CanReadType(Type type)
        {
            return true;
        }

        /// <summary>
        /// Queries whether this <see cref="T:System.Net.Http.Formatting.MediaTypeFormatter" /> can serializean object of the specified type.
        /// </summary>
        /// <param name="type">The type to serialize.</param>
        /// <returns>
        /// true if the <see cref="T:System.Net.Http.Formatting.MediaTypeFormatter" /> can serialize the type; otherwise, false.
        /// </returns>
        public override bool CanWriteType(Type type)
        {
            return true;
        }

        /// <summary>
        /// Asynchronously deserializes an object of the specified type.
        /// </summary>
        /// <param name="type">The type of the object to deserialize.</param>
        /// <param name="readStream">The <see cref="T:System.IO.Stream" /> to read.</param>
        /// <param name="content">The <see cref="T:System.Net.Http.HttpContent" />, if available. It may be null.</param>
        /// <param name="formatterLogger">The <see cref="T:System.Net.Http.Formatting.IFormatterLogger" /> to log events to.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> whose result will be an object of the given type.
        /// </returns>
        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            return Task.FromResult(ReadFromStream(type, readStream));
        }

        /// <summary>
        /// Asynchronously writes an object of the specified type.
        /// </summary>
        /// <param name="type">The type of the object to write.</param>
        /// <param name="value">The object value to write.  It may be null.</param>
        /// <param name="writeStream">The <see cref="T:System.IO.Stream" /> to which to write.</param>
        /// <param name="content">The <see cref="T:System.Net.Http.HttpContent" /> if available. It may be null.</param>
        /// <param name="transportContext">The <see cref="T:System.Net.TransportContext" /> if available. It may be null.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> that will perform the write.
        /// </returns>
        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            WriteToStream(value, writeStream);
            return Task.FromResult(true);
        }

        private object ReadFromStream(Type type, Stream readStream)
        {
            var streamReader = new StreamReader(readStream, _encoding);
            var serializer = new XmlSerializer(type);
            return serializer.Deserialize(streamReader);
        }

        private void WriteToStream(object value, Stream writeStream)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("atom", "http://www.w3.org/2005/Atom");

            var streamWriter = new StreamWriter(writeStream, _encoding);
            var serializer = new XmlSerializer(value.GetType());
            serializer.Serialize(streamWriter, value, namespaces);
        }
    }
}