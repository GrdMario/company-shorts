namespace Company.Shorts.Integration.Tests.Internal.Fixtures
{
    using Company.Shorts.Integration.Tests.Internal.Postgres;
    using DotNet.Testcontainers.Builders;
    using System;
    using Testcontainers.PostgreSql;

    public class PostgresDatabaseFixture : IDisposable
    {
        private bool _disposed;
        private IEnviromentVariableManager eventVariableManager = new PostgresEnviromentVariableManager();

        public PostgresDatabaseFixture()
        {
            PqsqlDatabase = new PostgreSqlBuilder()
                .WithImage(PostgreSqlContainerConstants.Image)
                .WithDatabase(PostgreSqlContainerConstants.Database)
                .WithUsername(PostgreSqlContainerConstants.Username)
                .WithPassword(PostgreSqlContainerConstants.Password)
                .WithExposedPort(5432)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(PostgreSqlContainerConstants.Port))
                .Build();

            PqsqlDatabase.StartAsync().Wait();

            this.eventVariableManager.Set(PqsqlDatabase.GetConnectionString());
        }

        public PostgreSqlContainer PqsqlDatabase { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    PqsqlDatabase.DisposeAsync().AsTask().Wait();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}