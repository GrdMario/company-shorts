namespace Company.Shorts.Presentation.Api.Controllers.V1
{
    using AutoMapper;
    using Company.Shorts.Presentation.Api.Internal.Constants;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiVersion(ApiVersions.V1)]
    public class ConfigurationsController : ApiControllerBase
    {
        private readonly IConfiguration configuration;
        public ConfigurationsController(IMediator mediator, IMapper mapper, IConfiguration configuration) : base(mediator, mapper)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Gets examples.
        /// </summary>
        /// <param name="query">Model for quering examples.</param>
        [HttpGet]
        [SwaggerOperation(OperationId = nameof(Get), Tags = new[] { ApiTags.Configurations })]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromQuery] string key)
        {
            var result = this.configuration.GetSection(key);

            return Ok(result);
        }
    }
}
