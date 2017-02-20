using Ploeh.AutoFixture;

namespace TemplateProject.Tests.Acceptence.WebApi.Infrastructure
{
    internal class TestAutoFixture : Fixture
    {
        public TestAutoFixture()
        {
            Customize(new HttpClientCustomization());
        }
    }
}