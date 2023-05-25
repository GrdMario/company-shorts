namespace Company.Shorts.Presentation.Api.Controllers.V1
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Company.Shorts.Presentation.Api.Internal.Constants;
    using Swashbuckle.AspNetCore.Annotations;
    using Company.Shorts.Application.UserAggregate.Query;
    using Company.Shorts.Domain;
    using Company.Shorts.Application.UserAggregate.Command;
    using Company.Shorts.Presentation.Api.Controllers.V1.Models.Users;

    [ApiVersion(ApiVersions.V1)]
    public class UsersController : ApiControllerBase
    {
        public UsersController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
        {
        }

        /// <summary>
        /// Gets users.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(OperationId = nameof(Get), Tags = new[] { ApiTags.Users })]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetUsersQueryDto request)
        {
            return await ProcessAsync<GetUsersQueryDto, GetUsersQuery, List<User>>(request);
        }

        /// <summary>
        /// Creates user.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(OperationId = nameof(Post), Tags = new[] { ApiTags.Users })]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateUserCommandDto request)
        {
            return await ProcessAsync<CreateUserCommandDto, CreateUserCommand>(request);
        }
    }
}