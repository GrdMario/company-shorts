namespace Company.Shorts.Blocks.Bootstrap
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public static class ApplicationLauncher
    {

        public static async Task<int> RunAsync<TStartup>(string[] args)
            where TStartup : class
        {
            var builder = new ConfigurationBuilder();

            BuildConfiguration<TStartup>(builder, args);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .CreateBootstrapLogger();

            try
            {
                Log.Information("Starting web host.");
                await CreateHostBuilder<TStartup>(args).Build().RunAsync();
                Log.Information("Web host started.");
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

        private static void BuildConfiguration<TStartup>(IConfigurationBuilder builder, string[] args)
            where TStartup : class
        {
            var environment = Environment.GetEnvironmentVariable(HostEnvironment.Variable);

            var isDevelopment = string.IsNullOrWhiteSpace(environment)
                || string.Equals(environment, HostEnvironment.Development, StringComparison.OrdinalIgnoreCase);

            builder
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(
                    path: "appsettings.json",
                    optional: false,
                    reloadOnChange: true)
                .AddJsonFile(
                    path: $"appsettings.{environment}.json",
                    optional: true,
                    reloadOnChange: true)
                .AddEnvironmentVariables();

            if (isDevelopment)
            {
                builder.AddUserSecrets<TStartup>();
            }

            builder.AddCommandLine(args);
        }
    }
}