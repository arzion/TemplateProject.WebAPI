using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.SelfHost;
using Ploeh.AutoFixture;
using TemplateProject.WebApi;

namespace TemplateProject.Tests.Acceptence.WebApi.Infrastructure
{
    internal class HttpClientCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var baseAddress = new Uri("http://localhost:8888");

            fixture.Register(
                () =>
                {
                    var config = new HttpSelfHostConfiguration(baseAddress);
                    Bootstrap.Configure(config);
                    SuppressErrorsHiding(config);

                    var server = new HttpSelfHostServer(config);
                    var httpClient = new HttpClient(server) { BaseAddress = baseAddress };
                    return httpClient;
                });
        }

        private static void SuppressErrorsHiding(HttpConfiguration configuration)
        {
            configuration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}