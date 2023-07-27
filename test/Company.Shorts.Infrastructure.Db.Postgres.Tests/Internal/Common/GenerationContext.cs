namespace Company.Shorts.Integration.Db.Postgres.Internal.Common
{
    using System.Collections.Generic;

    public sealed class GenerationContext
    {
        public GenerationContext(Dictionary<string, TableContext> database)
        {
            Database = database;
        }

        public Dictionary<string, TableContext> Database { get; set; }
    }
}