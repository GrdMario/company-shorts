namespace Company.Shorts.Integration.Tests.Internal
{
    public interface ISeedDatabaseManager
    {
        void Execute(string command);
    }
}