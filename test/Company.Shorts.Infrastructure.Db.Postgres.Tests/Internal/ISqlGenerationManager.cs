namespace Company.Shorts.Integration.Db.Postgres.Internal
{
    using Company.Shorts.Integration.Db.Postgres.Internal.Common;
    using System.Collections.Generic;

    public interface ISqlGenerationManager
    {
        GenerationResult Generate(Dictionary<string, object> obj);
    }
}