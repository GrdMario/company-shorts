namespace Company.Shorts.Infrastructure.Antivirus
{
    using Company.Shorts.Application.Contracts.Security;
    using Company.Shorts.Infrastructure.Antivirus.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using nClam;

    public static class DependencyInjection
    {
        public static IServiceCollection AddAntivirusLayer(this IServiceCollection services, AntivirusAdapterSettings settings)
        {
            services.AddScoped<ClamClient>(provider =>
            {
                var clamClient = new ClamClient(settings.Server, settings.Port);

                return clamClient;
            });

            services.AddScoped<IAntivirusService, AntivirusService>();

            return services;
        }
    }

    public class AntivirusAdapterSettings
    {
        public const string Key = nameof(AntivirusAdapterSettings);

        public string Server { get; set; } = default!;

        public int Port { get; set; }
    }
}