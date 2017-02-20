using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TemplateProject.WebAPI.AppStart.Formatters
{
    public class XmlMediaTypeFormatter : MediaTypeFormatter
    {
        private readonly UTF8Encoding _encoding = new UTF8Encoding(false, true);

        public XmlMediaTypeFormatter(params MediaTypeHeaderValue[] supportedMediaTypes)
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/xml"));

            foreach (var mediaTypeHeaderValue in supportedMediaTypes)
                SupportedMediaTypes.Add(mediaTypeHeaderValue);
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            return Task.FromResult(ReadFromStream(type, readStream));
        }

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