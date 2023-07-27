namespace Company.Shorts.Integration.Db.Postgres.Internal
{
    using System;

    public static class EnvironmentUtils
    {
        public static void SetTestEnvironment()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "development");
        }
    }
}