namespace Company.Shorts.Integration.Tests.Internal
{
    using Company.Shorts.Integration.Tests.Internal.Common;

    public interface ISqlScriptGenerator
    {
        string GenerateInsertScript(GenerationContext context);

        string GenerateDeleteScript(GenerationContext context);
    }
}