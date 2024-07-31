using BTS.Core.Commands.Models;
using BTS.Core.Results;
using BTS.Core.Results.Errors;
using BTS.Domain.Extensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BTS.Core.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : Result
    {
        private readonly IServiceProvider _service;
        public ValidationPipelineBehavior(IServiceProvider service) => _service = service; 

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Skip pipeline once the request name is not ends with Command
            if (CommandExtension.IsNotCommand<TRequest>()) return await next();

            // Get validator service based on the TRequest
            var validator = _service.GetRequiredService<IValidator<TRequest>>();
            if (validator == null) return await next();

            // Validate request
            ValidationResult validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return (TResponse)Result.Failure(CommonError.ValidationFailure<CreateDriverCommand>(validationResult.Errors));

            return await next();
        }
    }
}
