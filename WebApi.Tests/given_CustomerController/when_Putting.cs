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
    class when_PuttingData
    {
        [Test]
        public async Task then_NotFound_if_IdIsInvalid()
        {
            // arrange
            const int invalidId = 100500;

            var putResponse = await new HttpRequestBuilder()
                .PutAsync($"/customer/{invalidId}");

            // assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task then_OkReturned()
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

            // PUT
            var location = postResponse.Headers.Location;
            var putResponse = await new HttpRequestBuilder()
                .WithContent("{\"firstName\": \"new name\", \"lastName\": \"new last name\"}")
                .WithContentMediaType("application/quotemycad.customer-update+json")
                .PutAsync(location.AbsoluteUri);

            // assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test]
        public async Task then_CustomerUpdated()
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

            var newFirstName = fixture.Create<string>();
            var newLastName = fixture.Create<string>();

            // PUT
            var location = postResponse.Headers.Location;
            var putResponse = await new HttpRequestBuilder()
                .WithContent("{\"firstName\": \"" + newFirstName + "\", \"lastName\": \"" + newLastName + "\"}")
                .WithContentMediaType("application/quotemycad.customer-update+json")
                .PutAsync(location.AbsoluteUri);

            // assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // GET
            var getResponse = await new HttpRequestBuilder()
                .GetAsync(location.AbsoluteUri);

            // assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await getResponse.Content.ReadAsAsync<dynamic>();
            ((string)content.FirstName).Should().Be(newFirstName);
            ((string)content.LastName).Should().Be(newLastName);
        }

        [Test]
        public async Task then_UnsupportedMediaType_if_MediaTypeInvalid()
        {
            // arrange
            var fixture = new TestAutoFixture();
            var firstName = fixture.Create<string>();
            var lastName = fixture.Create<string>();

            var putResponse = await new HttpRequestBuilder()
                .WithContent("{\"firstName\": \"" + firstName + "\", \"lastName\": \"" + lastName + "\"}")
                .WithContentMediaType("application/quotemycad.fake-update+json")
                .PutAsync("/customer/100500");

            // assert
            putResponse.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
        }
    }
}