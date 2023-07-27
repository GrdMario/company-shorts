namespace Company.Shorts.Infrastructure.Http.ExternalApi.Tests
{
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal.Fixtures;
    using Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal.MockWebServer;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using System.Threading.Tasks;
    using Xunit;

    [Collection(CollectionFixtureConstants.Integration)]
    public class PetsTwoTest
    {
        private readonly IExternalApi externalApi;

        public PetsTwoTest(MockWebServerFixture fixture)
        {
            this.externalApi = fixture.Provider.GetRequiredService<IExternalApi>();
        }

        [Fact]
        [MockWebServerSeed("/Resources/ExternalApi/get-pets-expectation.json")]
        public async Task Test_One()
        {
            var response = await this.externalApi.GetAsync(default);

            response?.Count.Should().Be(2);
        }

        [Fact]
        [MockWebServerSeed("/Resources/ExternalApi/get-pets-expectation.json")]
        public async Task Test_Two()
        {
            var response = await this.externalApi.GetAsync(default);

            response?.Count.Should().Be(2);
        }
    }
}