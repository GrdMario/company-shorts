namespace Company.Shorts.Presentation.Api.Controllers.V1
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Company.Shorts.Presentation.Api.Internal.Constants;
    using Swashbuckle.AspNetCore.Annotations;
    using Company.Shorts.Application.HttpContextAggregate.Query;
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.HttpContextAccessors;
    using Company.Shorts.Blocks.Common.Swagger.Configuration;

    [ApiVersion(ApiVersions.V1)]
    public class HttpContextAccessorController : ApiControllerBase
    {
        public HttpContextAccessorController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Gets name from header.
        /// </summary>
        /// <param name="query">Model.</param>
        [HttpGet]
        [SwaggerOperation(OperationId = nameof(Get), Tags = new[] { ApiTags.HttpContextAccessors })]
        [SwaggerHeader("x-name", "user", "user")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetNameQueryDto request)
        {
            return await ProcessAsync<GetNameQueryDto, GetNameQuery, string>(request);
        }
    }
}
