namespace Company.Shorts.Integration.Db.Postgres.Internal.Fixtures
{
    using Xunit;

    [CollectionDefinition(CollectionFixtureConstants.Integration)]
    public class IntegrationTestCollection
        : ICollectionFixture<PostgresDatabaseFixture>
    {
    }
}