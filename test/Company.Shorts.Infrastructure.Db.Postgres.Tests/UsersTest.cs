namespace Company.Shorts.Infrastructure.Db.Postgres.Tests
{
    using Company.Shorts.Application.Contracts.Db;
    using Company.Shorts.Infrastructure.Db.Postgres.Internal.Repositories;
    using Company.Shorts.Integration.Db.Postgres.Internal.Fixtures;
    using Company.Shorts.Integration.Db.Postgres.Internal.Postgres;
    using FluentAssertions;
    using Xunit;

    [Collection(CollectionFixtureConstants.Integration)]
    public class UsersTest
    {
        private readonly IUserRepository userRepository;

        public UsersTest(PostgresDatabaseFixture fixture)
        {

            this.userRepository = new UserRepository(fixture.DbContext);
        }

        [Fact]
        [PostgresSeed("/Resources/Users/get-users.json")]
        public async void Test_Example_One()
        {
            var users = await this.userRepository.GetUsersAsync();

            users.Count.Should().Be(2);
        }

        [Fact]
        [PostgresSeed("/Resources/Users/get-users.json")]
        public async void Test_Example_Two()
        {
            var users = await this.userRepository.GetUsersAsync();

            users.Count.Should().Be(2);
        }
    }
}