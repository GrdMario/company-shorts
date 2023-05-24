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
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System;

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

        public ExampleAdapterSettings? ExampleAdapterSettings =>
            Configuration
                .GetSection(ExampleAdapterSettings.Key)
                .Get<ExampleAdapterSettings>();

        public PostgresAdapterSettings? PostgresAdapterSettings =>
            Configuration
                .GetSection(PostgresAdapterSettings.Key)
                .Get<PostgresAdapterSettings>();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddHealthChecks();
            services.AddPostgresDatabaseLayer(PostgresAdapterSettings ?? throw new ArgumentException(nameof(PostgresAdapterSettings)));
            services.AddInfrastructureExampleAdapter(ExampleAdapterSettings ?? throw new ArgumentException(nameof(ExampleAdapterSettings)));
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
