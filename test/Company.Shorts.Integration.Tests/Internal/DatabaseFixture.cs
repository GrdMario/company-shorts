namespace Company.Shorts.Integration.Tests.Internal
{
    using DotNet.Testcontainers.Builders;
    using System;
    using Testcontainers.PostgreSql;

    public class DatabaseFixture : IDisposable
    {
        private bool _disposed;

        public DatabaseFixture()
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

            EnvironmentUtils.SetTestDatabaseConnectionString(PqsqlDatabase.GetConnectionString());
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