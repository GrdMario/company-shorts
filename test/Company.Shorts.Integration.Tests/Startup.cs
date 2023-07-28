namespace Company.Shorts.EndToEnd.Tests
{
    using Company.Shorts.EndToEnd.Tests.Internal.Fixtures;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PostgresDatabaseFixture>();
            services.AddSingleton<MockWebServerFixture>();
        }
    }
}