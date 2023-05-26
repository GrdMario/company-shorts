﻿namespace Company.Shorts.Integration.Tests.Internal
{
    using Company.Shorts.Integration.Tests.Internal.Common;
    using System.Collections.Generic;

    public interface ISqlGenerationManager
    {
        GenerationResult Generate(Dictionary<string, object> obj);
    }
}