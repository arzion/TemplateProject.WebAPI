using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Ploeh.AutoFixture;

namespace TemplateProject.Tests.Acceptence.WebApi.Infrastructure
{
    public class HttpRequestBuilder
    {
        private readonly HttpClient _httpClient;
        private readonly HttpRequestMessage _requestMessage;
        private readonly IFixture _fixture;

        public HttpRequestBuilder(IFixture fixture)
        {
            _fixture = fixture;
            _httpClient = _fixture.Create<HttpClient>();
            _requestMessage = new HttpRequestMessage();
        }

        public HttpRequestBuilder()
            : this(new TestAutoFixture())
        {
        }

        public IFixture GetFixture()
        {
            return _fixture;
        }

        public HttpRequestBuilder WithAcceptMediaType(string mediaType)
        {
            _httpClient.DefaultRequestHeaders.Accept.ParseAdd(mediaType);
            return this;
        }

        public HttpRequestBuilder WithContentMediaType(string mediaType)
        {
            _requestMessage.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue(mediaType);
            return this;
        }

        public HttpRequestBuilder WithContent(string body)
        {
            var content = new StringContent(body);
            _requestMessage.Content = content;
            return this;
        }

        public async Task<HttpResponseMessage> PostAsync(string url)
        {
            _requestMessage.RequestUri = new Uri("http://localhost:8888/" + RemoveFirstDash(url));
            _requestMessage.Method = HttpMethod.Post;

            return await _httpClient.SendAsync(_requestMessage);
        }
        
        public async Task<HttpResponseMessage> PutAsync(string url)
        {
            _requestMessage.RequestUri = new Uri("http://localhost:8888/" + RemoveFirstDash(url));
            _requestMessage.Method = HttpMethod.Put;

            return await _httpClient.SendAsync(_requestMessage);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            _requestMessage.RequestUri = new Uri("http://localhost:8888/" + RemoveFirstDash(url));
            _requestMessage.Method = HttpMethod.Delete;

            return await _httpClient.SendAsync(_requestMessage);
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            _requestMessage.RequestUri = new Uri(url.Contains("http://") ? url : "http://localhost:8888/" + RemoveFirstDash(url));
            _requestMessage.Method = HttpMethod.Get;

            var response = await _httpClient.SendAsync(_requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                if (response.Content == null)
                {
                    Console.WriteLine("Content is empty");
                }
                else
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
            }

            return response;
        }

        private static string RemoveFirstDash(string value)
        {
            return value[0] == '/' ? value.Substring(1) : value;
        }
    }
}
