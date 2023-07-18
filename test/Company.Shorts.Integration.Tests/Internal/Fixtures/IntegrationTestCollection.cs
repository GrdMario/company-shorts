namespace Company.Shorts.Integration.Tests.Internal.Fixtures
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