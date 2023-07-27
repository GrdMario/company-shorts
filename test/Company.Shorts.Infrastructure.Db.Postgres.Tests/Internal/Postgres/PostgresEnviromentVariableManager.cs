namespace Company.Shorts.Integration.Db.Postgres.Internal.Postgres
{
    using System;

    internal sealed class PostgresEnviromentVariableManager : IEnviromentVariableManager
    {
        private const string TestConnectionString = "PG_CONNECTION_STRING";

        public string Get()
        {
            return Environment.GetEnvironmentVariable(TestConnectionString) ?? throw new ArgumentNullException(nameof(TestConnectionString));
        }

        public void Set(string connectionString)
        {
            Environment.SetEnvironmentVariable(TestConnectionString, connectionString);
        }
    }
}