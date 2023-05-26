namespace Company.Shorts.Integration.Tests.Internal.Mssql
{
    using System;

    internal sealed class MssqlEnviromentVariableManager : IEnviromentVariableManager
    {
        private const string TestConnectionString = "MSSQL_CONNECTION_STRING";

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