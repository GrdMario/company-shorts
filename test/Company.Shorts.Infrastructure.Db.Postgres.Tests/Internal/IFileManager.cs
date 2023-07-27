namespace Company.Shorts.Integration.Db.Postgres.Internal
{
    public interface IFileManager
    {
        T Read<T>(string path);

        string Read(string path);
    }
}