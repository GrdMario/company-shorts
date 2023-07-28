namespace Company.Shorts.EndToEnd.Tests.Internal
{
    using Company.Shorts.EndToEnd.Tests.Internal.Common;
    using System.Collections.Generic;

    public interface ISqlGenerationManager
    {
        GenerationResult Generate(Dictionary<string, object> obj);
    }
}