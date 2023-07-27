namespace Company.Shorts.Integration.Db.Postgres.Internal.Common
{
    using Newtonsoft.Json.Linq;

    public sealed record Cell(string Path, JTokenType Type, object Value, string Name, string ParentPath);
}