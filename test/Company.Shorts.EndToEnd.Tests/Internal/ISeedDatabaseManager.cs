namespace Company.Shorts.EndToEnd.Tests.Internal
{
    public interface ISeedDatabaseManager
    {
        void Execute(string command);

        void Delete();
    }
}