﻿namespace Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal
{
    using System;

    public static class EnvironmentUtils
    {
        public static void SetTestEnvironment()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "development");
        }
    }
}