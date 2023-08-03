namespace Company.Shorts.Blocks.Application.Core.Behaviors
{
    using FluentValidation;
    using FluentValidation.Results;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;

    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            List<ValidationResult> errors = new();

            foreach (var validator in _validators)
            {
                var error = await validator.ValidateAsync(request, cancellationToken);

                if (error is not null)
                {
                    errors.Add(error);
                }
            }

            var failures = errors
                .SelectMany(result => result.Errors)
                .Where(failure => failure is not null)
                .GroupBy(failure => failure.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(failure => failure.ErrorMessage).ToArray());

            if (failures.Any())
            {
                throw new Common.Exceptions.ValidationException(failures);
            }

            return await next();
        }
    }
}