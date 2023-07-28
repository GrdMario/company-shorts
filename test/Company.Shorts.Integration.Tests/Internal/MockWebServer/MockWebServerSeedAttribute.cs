namespace Company.Shorts.EndToEnd.Tests.Internal.MockWebServer
{
    using Company.Shorts.EndToEnd.Tests.Internal;
    using Company.Shorts.EndToEnd.Tests.Internal.Common;
    using System;
    using System.Net.Http;
    using System.Reflection;
    using Xunit.Sdk;

    public sealed class MockWebServerSeedAttribute : BeforeAfterTestAttribute
    {
        private readonly IFileManager fileManager = new FileManager();

        private readonly HttpClient httpClient;

        private readonly IEnviromentVariableManager enviromentVariableManager = new MockWebServerEnvironmentVariableManager();

        public MockWebServerSeedAttribute(string filePath)
        {
            this.FilePath = filePath;
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(this.enviromentVariableManager.Get())
            };
        }

        public string FilePath { get; }

        public override void After(MethodInfo methodUnderTest)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/mockserver/reset");

            var response = this.httpClient.Send(message);

            response.EnsureSuccessStatusCode();

            base.After(methodUnderTest);
        }

        public override void Before(MethodInfo methodUnderTest)
        {
            var json = this.fileManager.Read(this.FilePath);

            var message = new HttpRequestMessage(HttpMethod.Put, "/mockserver/expectation")
            {
                Content = new StringContent(json)
            };

            var response = this.httpClient.Send(message);

            response.EnsureSuccessStatusCode();

            base.Before(methodUnderTest);
        }
    }
}