namespace Company.Shorts.Presentation.Api.Controllers.V2
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Company.Shorts.Application.ExampleAggregate.Command;
    using Company.Shorts.Application.ExampleAggregate.Common.Responses;
    using Company.Shorts.Application.ExampleAggregate.Query;
    using Company.Shorts.Presentation.Api.Controllers.V2.Models.Examples;
    using Company.Shorts.Presentation.Api.Internal.Constants;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiVersion(ApiVersions.V2)]
    public class ExamplesController : ApiControllerBase
    {
        public ExamplesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Gets examples.
        /// </summary>
        /// <remarks>This is just a simple remark.</remarks>
        /// <param name="request">Model for quering examples.</param>
        /// <response code="200">Successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [SwaggerOperation(OperationId = nameof(Get), Tags = new[] { ApiTags.Examples })]
        [ProducesResponseType(typeof(List<ExampleResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetExamplesQueryDto request)
        {
            return await ProcessAsync<GetExamplesQueryDto, GetExamplesQuery, List<ExampleResponse>, List<ExampleResponseDto>>(request);
        }

        /// <summary>
        /// Creates example.
        /// </summary>
        /// <param name="request">Model for creating an example.</param>
        /// <response code="200">Successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerOperation(OperationId = nameof(Post), Tags = new[] { ApiTags.Examples })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateExampleCommandDto request)
        {
            return await ProcessAsync<CreateExampleCommandDto, CreateExampleCommand>(request);
        }

        /// <summary>
        /// Updates example.
        /// </summary>        
        /// <response code="200">Successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}")]
        [SwaggerOperation(OperationId = nameof(Put), Tags = new[] { ApiTags.Examples })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateExampleCommandDto request)
        {
            return await ProcessAsync<UpdateExampleCommandDto, UpdateExampleCommand>(request, opt => opt.AfterMap((_, command) =>
            {
                command.Id = id;
            }));
        }

        /// <summary>
        /// Gets example by id.
        /// </summary>
        /// <response code="200">Successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [SwaggerOperation(OperationId = nameof(GetById), Tags = new[] { ApiTags.Examples })]
        [ProducesResponseType(typeof(ExampleResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await ProcessAsync<GetExampleQueryDto, GetExampleQuery, ExampleResponse, ExampleResponseDto>(new GetExampleQueryDto(id));
        }
    }
}
