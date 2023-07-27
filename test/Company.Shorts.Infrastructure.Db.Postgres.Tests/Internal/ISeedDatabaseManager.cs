namespace Company.Shorts.Integration.Db.Postgres.Internal
{
    public interface ISeedDatabaseManager
    {
        void Execute(string command);

        void Delete();
    }
}