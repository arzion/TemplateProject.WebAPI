using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TemplateProject.Tests.Acceptence.WebApi.Infrastructure;
using Ploeh.AutoFixture;

namespace TemplateProject.Tests.Acceptence.WebApi.given_CustomerController
{
    // ReSharper disable once InconsistentNaming
    class when_GettingData
    {
        [Test]
        public async Task then_NotFound_if_IdInvalid()
        {
            // arrange
            const int invalidId = 100500;

            var getResponse = await new HttpRequestBuilder()
                .GetAsync($"/customer/{invalidId}");

            // assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task then_CustomerReturnedWithCorrectAttributes()
        {
            // arrange
            var fixture = new TestAutoFixture();
            var firstName = fixture.Create<string>();
            var lastName = fixture.Create<string>();

            // POST
            var postResponse = await new HttpRequestBuilder()
                .WithContent("{\"firstName\": \"" + firstName + "\", \"lastName\": \"" + lastName + "\"}")
                .WithContentMediaType("application/quotemycad.customer-create+json")
                .PostAsync("/customer");

            // assert
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // GET
            var location = postResponse.Headers.Location;
            var getResponse = await new HttpRequestBuilder()
                .GetAsync(location.AbsoluteUri);

            // assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getResponse.Content.Headers.ContentType.MediaType.Should().Be("application/quotemycad.customer+json");

            var content = await getResponse.Content.ReadAsAsync<dynamic>();
            ((string)content.FirstName).Should().Be(firstName);
            ((string)content.LastName).Should().Be(lastName);
        }
    }
}