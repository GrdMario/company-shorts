namespace Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal.MockWebServer
{
    using System;

    internal sealed class MockWebServerEnvironmentVariableManager : IEnviromentVariableManager
    {
        private const string TestConnectionString = "MWS_ConnectionString";

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
