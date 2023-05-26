namespace Company.Shorts.Integration.Tests
{
    using Company.Shorts.Integration.Tests.Internal;
    using System.Threading.Tasks;
    using Xunit;

    [Collection(CollectionFixtureConstants.Integration)]
    public class ExamplesTest
    {
        private WebApplicationFactoryFixture app;
        public ExamplesTest(WebApplicationFactoryFixture app)
        {
            this.app = app;
        }

        [Fact]
        public async Task Test2()
        {

            var response = await this.app.HttpClient.GetAsync("/api/v1/users");

        }
    }
}