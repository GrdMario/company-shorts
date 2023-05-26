namespace Company.Shorts.Integration.Tests.Internal
{
    using Npgsql;

    public static class DbConnectionUtility
    {
        public static void Execute(string command)
        {
            var connectionString = EnvironmentUtils.GetTestDatabaseConnectionString();

            using var connection = new NpgsqlConnection(connectionString);

            connection.Open();

            var cmd = new NpgsqlCommand(command, connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}