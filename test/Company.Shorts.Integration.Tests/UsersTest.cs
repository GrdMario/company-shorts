namespace Company.Shorts.Integration.Tests
{
    using Company.Shorts.Integration.Tests.Internal.Fixtures;
    using Company.Shorts.Integration.Tests.Internal.Postgres;
    using System.Threading.Tasks;
    using Xunit;

    [Collection(CollectionFixtureConstants.Integration)]
    public class UsersTest
    {
        private WebApplicationFactoryFixture app;

        public UsersTest(WebApplicationFactoryFixture app)
        {
            this.app = app;
        }

        [Fact]
        [PostgresSeed("/Resources/Users/GetUsers.json")]
        public async Task Test1()
        {
            var response = await this.app.HttpClient.GetAsync("/api/v1/users");

            var x = await response.Content.ReadAsStringAsync();
        }
    }
}