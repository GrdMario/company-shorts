namespace Company.Shorts.Integration.Tests.Internal.Postgres
{
    using Npgsql;

    internal sealed class PostgresSeedDatabaseManager : ISeedDatabaseManager
    {
        private readonly IEnviromentVariableManager enviromentVariableManager = new PostgresEnviromentVariableManager();

        public void Execute(string command)
        {
            // TODO: Wrap in try catch
            var connectionString = this.enviromentVariableManager.Get();

            using var connection = new NpgsqlConnection(connectionString);

            connection.Open();

            var cmd = new NpgsqlCommand(command, connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }
    }
}