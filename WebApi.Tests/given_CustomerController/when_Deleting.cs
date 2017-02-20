using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using TemplateProject.Tests.Acceptence.WebApi.Infrastructure;
using Ploeh.AutoFixture;

namespace TemplateProject.Tests.Acceptence.WebApi.given_CustomerController
{
    // ReSharper disable once InconsistentNaming
    class when_Deleting
    {
        [Test]
        public async Task then_OkStatusReturned()
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

            // DELETE
            var location = postResponse.Headers.Location;
            var deleteResponse = await new HttpRequestBuilder()
                .DeleteAsync(location.AbsoluteUri);

            // assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task then_NotFound_if_IdIsInvalid()
        {
            // arrange
            const int invalidId = 100500;

            var deleteResponse = await new HttpRequestBuilder()
                .DeleteAsync($"/customer/{invalidId}");

            // assert
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task then_NotFoundAfterDeleting()
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

            var location = postResponse.Headers.Location;

            var getResponse = await new HttpRequestBuilder()
                .GetAsync(location.AbsoluteUri);

            // assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            await new HttpRequestBuilder().DeleteAsync(location.AbsoluteUri);

            // act
            getResponse = await new HttpRequestBuilder()
                .GetAsync(location.AbsoluteUri);

            // assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}