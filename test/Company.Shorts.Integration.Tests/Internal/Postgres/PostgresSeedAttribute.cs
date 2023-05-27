namespace Company.Shorts.Integration.Tests.Internal.Postgres
{
    using Company.Shorts.Integration.Tests.Internal;
    using Company.Shorts.Integration.Tests.Internal.Common;
    using System.Reflection;
    using Xunit.Sdk;

    public sealed class PostgresSeedAttribute : BeforeAfterTestAttribute
    {
        private GenerationResult generationItem = default!;

        private readonly ISeedDatabaseManager dbConnectionUtility = new PostgresSeedDatabaseManager();

        private readonly ISqlGenerationManager sqlGenerationManager = new PostgresSqlGenerationManager();

        private readonly IFileManager fileManager = new FileManager();

        public PostgresSeedAttribute(string filePath)
        {
            this.FilePath = filePath;
        }

        public string FilePath { get; }

        public override void After(MethodInfo methodUnderTest)
        {
            this.dbConnectionUtility.Delete();

            base.After(methodUnderTest);
        }

        public override void Before(MethodInfo methodUnderTest)
        {
            var obj = this.fileManager.Read(this.FilePath);

            this.generationItem = this.sqlGenerationManager.Generate(obj);

            this.dbConnectionUtility.Execute(generationItem.Insert);

            base.Before(methodUnderTest);
        }
    }
}