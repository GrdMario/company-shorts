namespace Company.Shorts.Integration.Tests
{
    using Company.Shorts.Integration.Tests.Internal;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DatabaseFixture>();
            services.AddSingleton<WebApplicationFactoryFixture>();
        }
    }
}