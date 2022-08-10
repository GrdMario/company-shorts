namespace Company.Shorts.Blocks.Bootstrap
{
    using Azure.Extensions.AspNetCore.Configuration.Secrets;
    using Azure.Identity;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static partial class ApplicationLauncher
    {
        public static async Task<int> RunAsync<TStartup>(string[] args)
            where TStartup : class
        {
            var builder = new ConfigurationBuilder();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .CreateBootstrapLogger();

            try
            {
                Log.Information("Starting web host.");

                var host = CreateHostBuilder<TStartup>(args)
                    .ConfigureAppConfiguration((hostBuildContext, configurationBuilder) =>
                {
                    BuildConfiguration<TStartup>(configurationBuilder, hostBuildContext.HostingEnvironment, args);
                });

                await host.Build().RunAsync();

                return ExitCode.Success;
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Host terminated unexpectedly.");
                return ExitCode.Failure;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder<TStartup>(string[] args)
            where TStartup : class
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((hostContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration))
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<TStartup>());
        }

        private static void BuildConfiguration<TStartup>(
            IConfigurationBuilder builder,
            IHostEnvironment environment,
            string[] args)
            where TStartup : class
        {
            builder
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    path: "appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .AddJsonFile(
                    path: $"appsettings.{environment.EnvironmentName}.json",
                    optional: true,
                    reloadOnChange: true)
                .AddEnvironmentVariables();

            if (environment.IsDevelopment())
            {
                builder.AddUserSecrets<TStartup>();
            }

            if (environment.IsProduction())
            {
                var keyVaultName = builder.Build().GetSection("Azure:KeyVault:Name").Value;

                builder.AddAzureKeyVault(
                    new Uri($"https://{keyVaultName}.vault.azure.net/"),
                    new DefaultAzureCredential(new DefaultAzureCredentialOptions
                    {
                        ExcludeAzureCliCredential = true,
                        ExcludeAzurePowerShellCredential = true,
                        ExcludeEnvironmentCredential = true,
                        ExcludeInteractiveBrowserCredential = true,
                        ExcludeManagedIdentityCredential = true,
                        ExcludeSharedTokenCacheCredential = true,
                        ExcludeVisualStudioCodeCredential = true,
                        ExcludeVisualStudioCredential = false
                    }),
                    new AzureKeyVaultConfigurationOptions()
                    {
                        Manager = new PrefixKeyVaultSecretManager(environment.EnvironmentName, environment.ApplicationName)
                    });
            }

            builder.AddCommandLine(args);

            builder.Build();
        }
    }
}