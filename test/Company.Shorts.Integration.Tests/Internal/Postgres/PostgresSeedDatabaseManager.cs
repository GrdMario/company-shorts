namespace Company.Shorts.Integration.Tests.Internal.Postgres
{
    using Npgsql;
    using System.Collections.Generic;

    internal sealed class PostgresSeedDatabaseManager : ISeedDatabaseManager
    {
        private readonly IEnviromentVariableManager enviromentVariableManager = new PostgresEnviromentVariableManager();

        public void Execute(string command)
        {
            var connectionString = this.enviromentVariableManager.Get();

            using var connection = new NpgsqlConnection(connectionString);

            connection.Open();

            var cmd = new NpgsqlCommand(command, connection);

            cmd.ExecuteNonQuery();

            connection.Close();
        }

        public void Delete()
        {
            var connectionString = this.enviromentVariableManager.Get();

            List<string> toDelete = new();

            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var cmd = new NpgsqlCommand("select tablename from pg_catalog.pg_tables where schemaname = 'public' and tablename not like '__EFMigrationsHistory'", connection);

            using var reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                toDelete.Add(reader.GetString(0));
            }

            reader.Close();

            foreach(var item in toDelete)
            {
                var delete = new NpgsqlCommand($"DELETE FROM {item}", connection);
                delete.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
}