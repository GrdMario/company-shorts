namespace Company.Shorts.Blocks.Presentation.Api.Configuration
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Routing;

    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointRouteBuilder MapDefaultHealthCheckRoute(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapHealthChecks("/health");
            return endpoint;
        }
    }
}