namespace Company.Shorts.Integration.Tests.Internal
{
    public static class PostgreSqlContainerConstants
    {
        public static string Image = "postgres:15.1-alpine";
        public static string Database = "ShortsUserDb";
        public static string Username = "postgres";
        public static string Password = "postgres";
        public static int Port = 5432;
    }

    public static class CollectionFixtureConstants
    {
        public const string Integration = "Integration";
    }
}