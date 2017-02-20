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
    class when_PostingData
    {
        [Test]
        public async Task then_CreatedStatusReturned()
        {
            // arrange
            var fixture = new TestAutoFixture();
            var firstName = fixture.Create<string>();
            var lastName = fixture.Create<string>();

            // POST
            var postResponse = await new HttpRequestBuilder()
                .WithContent("{\"firstName\": \"" + firstName + "\", \"lastName\": \"" + lastName + "\"}")
                .WithContentMediaType("application/quotemycad.customer-create+json")
                .PostAsync("/customers");

            // assert
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Test]
        public async Task then_CustomerCreated_and_CreatedCustomerHasCorrectAttributes()
        {
            // arrange
            var fixture = new TestAutoFixture();
            var firstName = fixture.Create<string>();
            var lastName = fixture.Create<string>();

            // POST
            var postResponse = await new HttpRequestBuilder()
                .WithContent("{\"firstName\": \"" + firstName + "\", \"lastName\": \"" + lastName + "\"}")
                .WithContentMediaType("application/quotemycad.customer-create+json")
                .PostAsync("/customers");

            // assert
            postResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            // GET
            var location = postResponse.Headers.Location;
            var getResponse = await new HttpRequestBuilder()
                .GetAsync(location.AbsoluteUri);

            // assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await getResponse.Content.ReadAsAsync<dynamic>();
            ((string)content.FirstName).Should().Be(firstName);
            ((string)content.LastName).Should().Be(lastName);
        }

        [Test]
        public async Task then_UnsupportedMediaType_if_MediaTypeInvalid()
        {
            // arrange
            var fixture = new TestAutoFixture();
            var firstName = fixture.Create<string>();
            var lastName = fixture.Create<string>();

            // POST
            var postResponse = await new HttpRequestBuilder()
                .WithContent("{\"firstName\": \"" + firstName + "\", \"lastName\": \"" + lastName + "\"}")
                .WithContentMediaType("application/quotemycad.fake-create+json")
                .PostAsync("/customers");

            // assert
            postResponse.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
        }

        [Test]
        public async Task then_UnsupportedMediaType_if_MediaTypeNotSpecified()
        {
            // arrange
            var fixture = new TestAutoFixture();
            var firstName = fixture.Create<string>();
            var lastName = fixture.Create<string>();

            // POST
            var postResponse = await new HttpRequestBuilder()
                .WithContent("{\"firstName\": \"" + firstName + "\", \"lastName\": \"" + lastName + "\"}")
                .PostAsync("/customers");

            // assert
            postResponse.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
        }
    }
}