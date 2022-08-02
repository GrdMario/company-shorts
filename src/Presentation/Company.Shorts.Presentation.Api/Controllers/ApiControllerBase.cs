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
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        protected ApiControllerBase(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        protected async Task<IActionResult> ProcessAsync<TApiDto, TCommand, TResponse>(
            TApiDto request,
            Action<IMappingOperationOptions<object, TCommand>>? opts = null,
            CancellationToken cancellationToken = default)
            where TApiDto : IApiDto
            where TCommand : IRequest<TResponse>
        {
            TCommand? command = opts is not null
                ? _mapper.Map<TCommand>(request, opts)
                : _mapper.Map<TCommand>(request);

            TResponse result = await _mediator.Send(command, cancellationToken);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected async Task<IActionResult> ProcessAsync<TApiDto, TCommand>(
            TApiDto request,
            Action<IMappingOperationOptions<object, TCommand>>? opts = null,
            CancellationToken cancellationToken = default)
            where TApiDto : IApiDto
            where TCommand : IRequest
        {
            TCommand command = opts is not null
                ? _mapper.Map<TCommand>(request, opts)
                : _mapper.Map<TCommand>(request);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
