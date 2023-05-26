namespace Company.Shorts.Integration.Tests.Internal
{
    using System;

    public static class EnvironmentUtils
    {
        private const string TestConnectionString = "TEST_CONNECTION_STRING";

        public static void SetTestEnvironment()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "development");
        }

        public static void SetTestDatabaseConnectionString(string connectionString)
        {
            Environment.SetEnvironmentVariable(TestConnectionString, connectionString);
        }

        public static string GetTestDatabaseConnectionString()
        {
            return Environment.GetEnvironmentVariable(TestConnectionString) ?? throw new ArgumentNullException(nameof(TestConnectionString));
        }
    }
}