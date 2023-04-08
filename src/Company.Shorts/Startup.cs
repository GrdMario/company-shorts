namespace Company.Shorts
{
    using Company.Shorts.Application;
    using Company.Shorts.Blocks.Common.Mapping.Configuration;
    using Company.Shorts.Blocks.Common.Serilog.Configuration;
    using Company.Shorts.Blocks.Common.Swagger.Configuration;
    using Company.Shorts.Blocks.Presentation.Api.Configuration;
    using Company.Shorts.Infrastructure.Db.Postgres;
    using Company.Shorts.Infrastructure.ExampleAdapter;
    using Company.Shorts.Presentation.Api;
    using Hellang.Middleware.ProblemDetails;
    using Company.Shorts.Infrastructure.Cache;
    using Company.Shorts.Infrastructure.Cache.Redis;

    public sealed class Startup
    {
        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public ExampleAdapterSettings ExampleAdapterSettings =>
            Configuration
                .GetSection(ExampleAdapterSettings.Key)
                .Get<ExampleAdapterSettings>();

        public PostgresAdapterSettings PostgresAdapterSettings =>
            Configuration
                .GetSection(PostgresAdapterSettings.Key)
                .Get<PostgresAdapterSettings>();

        public RedisAdapterSettings ReddisAdapterSettings =>
            Configuration
                .GetSection(RedisAdapterSettings.Key)
                .Get<RedisAdapterSettings>();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddReddisCache(ReddisAdapterSettings);
            services.AddCors();
            services.AddHealthChecks();
            services.AddPostgresDatabaseLayer(PostgresAdapterSettings);
            services.AddInfrastructureExampleAdapter(ExampleAdapterSettings);
            services.AddApplicationLayer();
            services.AddPresentationLayer(Environment);
            services.AddAutoMapperConfiguration(AppDomain.CurrentDomain);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();

            if (!Environment.IsDevelopment())
            {
                app.UseHsts();
            }

            app.MigratePostgresDb();

            app.UseSwaggerConfiguration();

            app.UseHttpsRedirection();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSerilogConfiguration();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapDefaultHealthCheckRoute();
            });
        }
    }
}
