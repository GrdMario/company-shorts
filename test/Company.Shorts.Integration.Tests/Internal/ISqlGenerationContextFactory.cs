namespace Company.Shorts.Integration.Tests.Internal
{
    using Company.Shorts.Integration.Tests.Internal.Common;
    using System.Collections.Generic;

    public interface ISqlGenerationContextFactory
    {
        GenerationContext CreateContext(Dictionary<string, object> obj);
    }
}