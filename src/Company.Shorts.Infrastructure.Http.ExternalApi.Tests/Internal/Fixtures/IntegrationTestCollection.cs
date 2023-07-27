namespace Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal.Fixtures
{
    using Xunit;

    [CollectionDefinition(CollectionFixtureConstants.Integration)]
    public class IntegrationTestCollection:
         ICollectionFixture<MockWebServerFixture>
    {
    }
}