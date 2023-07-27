namespace Company.Shorts.Integration.Db.Postgres.Internal
{
    public interface IEnviromentVariableManager
    {
        string Get();

        void Set(string connectionString);
    }
}