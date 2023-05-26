namespace Company.Shorts.Integration.Tests
{
    using Company.Shorts.Integration.Tests.Internal.Fixtures;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<PostgresDatabaseFixture>();
            services.AddSingleton<WebApplicationFactoryFixture>();
        }
    }
}