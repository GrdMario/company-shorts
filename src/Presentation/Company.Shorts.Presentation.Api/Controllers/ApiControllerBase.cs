namespace Company.Shorts.Presentation.Api.Controllers
{
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Mime;
    using System.Threading;

    public interface IApiDto
    {
    }

    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ApiControllerBase(
            IMediator mediator)
        {
            this.Mediator = mediator;
        }

        public IMediator Mediator { get; }

        protected async Task<IActionResult> ProcessAsync<TCommand, TResponse>(
            TCommand command, CancellationToken cancellationToken)
            where TCommand : IRequest<TResponse>
        {
            TResponse result = await Mediator.Send(command, cancellationToken);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        protected async Task<IActionResult> ProcessAsync<TCommand>(
            TCommand command,
            CancellationToken cancellationToken)
            where TCommand : IRequest
        {
            await Mediator.Send(command, cancellationToken);

            return NoContent();
        }
    }
}
