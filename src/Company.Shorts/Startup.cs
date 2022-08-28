namespace Company.Shorts
{
    using Company.Shorts.Application;
    using Company.Shorts.Blocks.Common.Mapping.Configuration;
    using Company.Shorts.Blocks.Common.Serilog.Configuration;
    using Company.Shorts.Infrastructure.ExampleAdapter;
    using Company.Shorts.Presentation.Api;
    using Company.Shorts.Presentation.Api.Internal.Configuration.Swagger;
    using Hellang.Middleware.ProblemDetails;

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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddHealthChecks();
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
            });
        }
    }
}
