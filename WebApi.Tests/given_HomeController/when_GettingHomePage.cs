using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TemplateProject.Tests.Acceptence.WebApi.Infrastructure;

namespace TemplateProject.Tests.Acceptence.WebApi.given_HomeController
{
    // ReSharper disable once InconsistentNaming
    class when_GettingHomePage
    {
        [Test]
        public async Task then_OkStatusReturned()
        {
            // arrange
            var response = await new HttpRequestBuilder()
                .GetAsync("/");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
