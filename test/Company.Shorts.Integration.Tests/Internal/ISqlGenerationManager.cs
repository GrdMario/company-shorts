namespace Company.Shorts.Integration.Tests.Internal
{
    using Company.Shorts.Integration.Tests.Internal.Common;
    using System.Collections.Generic;

    public interface ISqlGenerationManager
    {
        GenerationItem Generate(Dictionary<string, object[]> obj);
    }
}