namespace Company.Shorts.EndToEnd.Tests.Internal
{
    using Company.Shorts.EndToEnd.Tests.Internal.Common;

    public interface ISqlScriptGenerator
    {
        string GenerateInsertScript(GenerationContext context);

        string GenerateDeleteScript(GenerationContext context);
    }
}