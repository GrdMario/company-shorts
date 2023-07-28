namespace Company.Shorts.EndToEnd.Tests.Internal.Fixtures
{
    using Company.Shorts.EndToEnd.Tests.Internal.MockWebServer;
    using DotNet.Testcontainers.Builders;
    using DotNet.Testcontainers.Containers;
    using System;

    public class MockWebServerFixture : IDisposable
    {
        private bool _disposed;

        private readonly IEnviromentVariableManager enviromentVariableManager = new MockWebServerEnvironmentVariableManager();

        public MockWebServerFixture()
        {
            this.MockWebServerContainer = new ContainerBuilder()
                .WithImage(MockWebServerConstants.Image)
                .WithPortBinding(MockWebServerConstants.Port)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("INFO 1080 started on port: 1080"))
                .Build();

            this.MockWebServerContainer.StartAsync().Wait();

            this.Url = $"http://{this.MockWebServerContainer.Hostname}:{this.MockWebServerContainer.GetMappedPublicPort(MockWebServerConstants.Port)}";

            this.enviromentVariableManager.Set(this.Url);
        }

        public IContainer MockWebServerContainer { get; }

        public string Url { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    this.MockWebServerContainer.DisposeAsync().AsTask().Wait();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}