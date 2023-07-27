namespace Company.Shorts.Integration.Db.Postgres.Internal
{
    using Company.Shorts.Integration.Db.Postgres.Internal.Common;

    public interface ISqlScriptGenerator
    {
        string GenerateInsertScript(GenerationContext context);

        string GenerateDeleteScript(GenerationContext context);
    }
}