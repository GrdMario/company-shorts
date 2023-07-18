namespace Company.Shorts.Integration.Tests
{
    using Company.Shorts.Application.PetsAggregate.Queries;
    using Company.Shorts.Integration.Tests.Internal.Fixtures;
    using Company.Shorts.Integration.Tests.Internal.MockWebServer;
    using FluentAssertions;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    [Collection(CollectionFixtureConstants.Integration)]
    public class PetsTest
    {
        private readonly WebApplicationFactoryFixture app;

        public PetsTest(WebApplicationFactoryFixture app)
        {
            this.app = app;
        }

        [Fact]
        [MockWebServerSeed("/Resources/ExternalApi/get-pets-expectation.json")]
        public async Task Test()
        {
            var response = await this.app.HttpClient.GetAsync("/api/v1/pets");

            var result = JsonConvert.DeserializeObject<List<PetResponse>>(await response.Content.ReadAsStringAsync());

            result?.Count.Should().Be(2);
        }
    }
}