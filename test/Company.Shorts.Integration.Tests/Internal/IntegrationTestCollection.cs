namespace Company.Shorts.Integration.Tests.Internal
{
    using Xunit;

    [CollectionDefinition(CollectionFixtureConstants.Integration)]
    public class IntegrationTestCollection
        : ICollectionFixture<DatabaseFixture>
        , ICollectionFixture<WebApplicationFactoryFixture>
    {
    }
}