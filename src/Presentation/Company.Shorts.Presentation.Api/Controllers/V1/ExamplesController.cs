namespace Company.Shorts.Presentation.Api.Controllers.V1
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Company.Shorts.Application.ExampleAggregate.Command;
    using Company.Shorts.Application.ExampleAggregate.Common.Responses;
    using Company.Shorts.Application.ExampleAggregate.Query;
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.Examples;
    using Company.Shorts.Presentation.Api.Internal.Constants;
    using Swashbuckle.AspNetCore.Annotations;

    [ApiVersion(ApiVersions.V1)]
    public class ExamplesController : ApiControllerBase
    {
        public ExamplesController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }
    
        [HttpGet]
        [SwaggerOperation(
            OperationId = nameof(Get),
            Tags = new[] { ApiTags.Examples },
            Description = "Gets a list of examples.",
            Summary = "Retrieve list of examples.")]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            Description = "Successfull operation.",
            Type = typeof(List<ExampleResponseDto>))]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            Description = "Bad request.",
            Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(
            StatusCodes.Status500InternalServerError,
            Description = "Internal server error.",
            Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get([FromQuery] GetExamplesQueryDto request)
        {
            return await ProcessAsync<GetExamplesQueryDto, GetExamplesQuery, List<ExampleResponse>, List<ExampleResponseDto>>(request);
        }

        [HttpPost]
        [SwaggerOperation(
            OperationId = nameof(Post),
            Description = "Creates new example.",
            Summary = "Creates new example.",
            Tags = new[] { ApiTags.Examples })]
        [SwaggerResponse(
            StatusCodes.Status204NoContent,
            Description = "Successfull operation.")]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            Description = "Bad request.",
            Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(
            StatusCodes.Status500InternalServerError,
            Description = "Internal server error.",
            Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Post([FromBody] CreateExampleCommandDto request)
        {
            return await ProcessAsync<CreateExampleCommandDto, CreateExampleCommand>(request);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            OperationId = nameof(Put),
            Description = "Updates an existing example by unique identifier.",
            Summary = "Updates existing example.",
            Tags = new[] { ApiTags.Examples })]
        [SwaggerResponse(
            StatusCodes.Status204NoContent,
            Description = "Successfull operation.")]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            Description = "Bad request.",
            Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(
            StatusCodes.Status500InternalServerError,
            Description = "Internal server error.",
            Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateExampleCommandDto request)
        {
            return await ProcessAsync<UpdateExampleCommandDto, UpdateExampleCommand>(request, opt => opt.AfterMap((_, command) =>
            {
                command.Id = id;
            }));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            OperationId = nameof(GetById),
            Tags = new[] { ApiTags.Examples },
            Description = "Retrieves an example by unique identifier.",
            Summary = "Retrieve an examples.")]
        [SwaggerResponse(
            StatusCodes.Status200OK,
            Description = "Successfull operation.",
            Type = typeof(ExampleResponseDto))]
        [SwaggerResponse(
            StatusCodes.Status400BadRequest,
            Description = "Bad request.",
            Type = typeof(ValidationProblemDetails))]
        [SwaggerResponse(
            StatusCodes.Status500InternalServerError,
            Description = "Internal server error.",
            Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetById(Guid id)
        {
            return await ProcessAsync<GetExampleQueryDto, GetExampleQuery, ExampleResponse, ExampleResponseDto>(new GetExampleQueryDto(id));
        }
    }
}
