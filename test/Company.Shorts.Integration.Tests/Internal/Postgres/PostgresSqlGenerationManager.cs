namespace Company.Shorts.EndToEnd.Tests.Internal.Postgres
{
    using Company.Shorts.EndToEnd.Tests.Internal;
    using Company.Shorts.EndToEnd.Tests.Internal.Common;
    using System.Collections.Generic;

    internal sealed class PostgresSqlGenerationManager : ISqlGenerationManager
    {
        private readonly ISqlGenerationContextFactory contextFactory = new SqlGenerationContextFactory();

        private readonly ISqlScriptGenerator sqlScriptGenerator = new PostgresSqlScriptGenerator();

        public GenerationResult Generate(Dictionary<string, object> obj)
        {
            var context = this.contextFactory.CreateContext(obj);

            var insert = this.sqlScriptGenerator.GenerateInsertScript(context);

            var delete = this.sqlScriptGenerator.GenerateDeleteScript(context);

            return new GenerationResult(insert, delete);
        }
    }
}