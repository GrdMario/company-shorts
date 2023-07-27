namespace Company.Shorts.Integration.Db.Postgres.Internal.Fixtures
{
    public static class PostgreSqlContainerConstants
    {
        public const string Image = "postgres:15.1-alpine";
        public const string Database = "ShortsUserDb";
        public const string Username = "postgres";
        public const string Password = "postgres";
        public const int Port = 5432;
    }
}