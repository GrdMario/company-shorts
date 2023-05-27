namespace Company.Shorts.Integration.Tests
{
    using Company.Shorts.Domain;
    using Company.Shorts.Integration.Tests.Internal.Fixtures;
    using Company.Shorts.Integration.Tests.Internal.Postgres;
    using FluentAssertions;
    using Newtonsoft.Json;
    using System.Collections.Generic;
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
        public async Task GetUsers_Should_ReturnTwoUsers()
        {
            var response = await this.app.HttpClient.GetAsync("/api/v1/users");

            var result = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());

            result?.Count.Should().Be(2);
        }
    }
}