namespace Company.Shorts.Presentation.Api.Controllers
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Mime;

    public interface IApiDto
    {
    }

    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly IMapper _mapper;

        protected ApiControllerBase(
            IMediator mediator,
            IMapper mapper)
        {
            this.Mediator = mediator;
            _mapper = mapper;
        }

        public IMediator Mediator { get; }

        protected async Task<IActionResult> ProcessAsync<TApiDto, TCommand, TResponse>(
            TApiDto request,
            Action<IMappingOperationOptions<object, TCommand>>? opts = null)
            where TApiDto : IApiDto
            where TCommand : IRequest<TResponse>
        {
            TCommand? command = opts is not null
                ? _mapper.Map<TCommand>(request, opts)
                : _mapper.Map<TCommand>(request);

            TResponse result = await this.Mediator.Send(command);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected async Task<IActionResult> ProcessAsync<TApiDto, TCommand>(
            TApiDto request,
            Action<IMappingOperationOptions<object, TCommand>>? opts = null)
            where TApiDto : IApiDto
            where TCommand : IRequest
        {
            TCommand command = opts is not null
                ? _mapper.Map<TCommand>(request, opts)
                : _mapper.Map<TCommand>(request);

            await this.Mediator.Send(command);

            return NoContent();
        }
    }
}
