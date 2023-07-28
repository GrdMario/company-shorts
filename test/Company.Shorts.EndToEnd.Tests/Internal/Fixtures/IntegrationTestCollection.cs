namespace Company.Shorts.EndToEnd.Tests.Internal.Fixtures
{
    using Xunit;

    [CollectionDefinition(CollectionFixtureConstants.Integration)]
    public class IntegrationTestCollection
        : ICollectionFixture<PostgresDatabaseFixture>,
        ICollectionFixture<MockWebServerFixture>,
        ICollectionFixture<WebApplicationFactoryFixture>
    {
    }
}