namespace Company.Shorts.Integration.Tests.Internal.Fixtures
{
    using Company.Shorts;
    using Microsoft.AspNetCore.Mvc.Testing;
    using System;
    using System.Net.Http;

    public class WebApplicationFactoryFixture : IDisposable
    {
        private bool _disposed;

        public WebApplicationFactoryFixture(PostgresDatabaseFixture fixture, MockWebServerFixture webServerFixture)
        {
            EnvironmentUtils.SetTestEnvironment();

            var application = new WebApplicationFactory<Program>().WithWebHostBuilder(conf =>
            {
                conf.UseSetting("PostgresAdapterSettings:Url", fixture.PqsqlDatabase.GetConnectionString());
                conf.UseSetting("ExternalApiSettings:Url", webServerFixture.Url);
            });

            HttpClient = application.CreateClient();
        }

        public HttpClient HttpClient { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    HttpClient.Dispose();
                }

                _disposed = true;
            }
        }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}