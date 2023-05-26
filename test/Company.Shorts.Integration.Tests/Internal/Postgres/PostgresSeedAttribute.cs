namespace Company.Shorts.Integration.Tests.Internal.Postgres
{
    using Company.Shorts.Integration.Tests.Internal;
    using Company.Shorts.Integration.Tests.Internal.Common;
    using System;
    using System.Reflection;
    using Xunit.Sdk;

    public sealed class PostgresSeedAttribute : BeforeAfterTestAttribute
    {
        private GenerationItem generationItem = default!;

        private readonly ISeedDatabaseManager dbConnectionUtility = new PostgresSeedDatabaseManager();

        private readonly ISqlGenerationManager sqlGenerationManager = new PostgresSqlGenerationManager();

        private readonly IFileManager fileManager = new FileManager();

        public PostgresSeedAttribute(string filePath)
        {
            // TODO: Add factory for resolving type of database.

            FilePath = filePath;
        }

        public string FilePath { get; }

        public override void After(MethodInfo methodUnderTest)
        {
            dbConnectionUtility.Execute(generationItem.Delete);

            base.After(methodUnderTest);
        }

        public override void Before(MethodInfo methodUnderTest)
        {
            var obj = fileManager.Read(this.FilePath);

            generationItem = sqlGenerationManager.Generate(obj);

            dbConnectionUtility.Execute(generationItem.Insert);

            base.Before(methodUnderTest);
        }
    }
}