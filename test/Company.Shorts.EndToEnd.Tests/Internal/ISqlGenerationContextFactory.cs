namespace Company.Shorts.EndToEnd.Tests.Internal
{
    using Company.Shorts.EndToEnd.Tests.Internal.Common;
    using System.Collections.Generic;

    public interface ISqlGenerationContextFactory
    {
        GenerationContext CreateContext(Dictionary<string, object> obj);
    }
}