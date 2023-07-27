namespace Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal.Fixtures
{
    using Company.Shorts.Application.Contracts.Http;
    using Company.Shorts.Infrastructure.Http.ExternalApi.Internal;
    using Company.Shorts.Infrastructure.Http.ExternalApi.Tests.Internal.MockWebServer;
    using DotNet.Testcontainers.Builders;
    using DotNet.Testcontainers.Containers;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class MockWebServerConstants
    {
        public const string Image = "mockserver/mockserver";
        public const int Port = 1080;
    }

    public class MockWebServerFixture : IDisposable
    {
        private bool _disposed;

        private readonly IEnviromentVariableManager enviromentVariableManager = new MockWebServerEnvironmentVariableManager();

        public MockWebServerFixture()
        {
            this.MockWebServerContainer = new ContainerBuilder()
                .WithImage(MockWebServerConstants.Image)
                .WithPortBinding(MockWebServerConstants.Port)
                //.WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MockWebServerConstants.Port))
                // For some reason, we are not able to use port strategy. This is a workaround
                .WithWaitStrategy(Wait.ForUnixContainer().UntilMessageIsLogged("INFO 1080 started on port: 1080"))
                .Build();

            this.MockWebServerContainer.StartAsync().Wait();

            this.Url = $"http://{this.MockWebServerContainer.Hostname}:{this.MockWebServerContainer.GetMappedPublicPort(MockWebServerConstants.Port)}";

            var services = new ServiceCollection();

            services.AddHttpClient<IExternalApi, ExternalApiService>(opt => opt.BaseAddress = new Uri(this.Url));

            this.Provider = services.BuildServiceProvider();

            this.enviromentVariableManager.Set(this.Url);
        }

        public ServiceProvider Provider { get; }

        public IContainer MockWebServerContainer { get; }

        public string Url { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    this.MockWebServerContainer.DisposeAsync().AsTask().Wait();
                    this.Provider.Dispose();
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