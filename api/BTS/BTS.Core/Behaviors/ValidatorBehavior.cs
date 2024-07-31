using BTS.Core.Commands.Models;
using BTS.Core.Results;
using BTS.Core.Results.Errors;
using BTS.Domain.Extensions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace BTS.Core.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : Result
    {
        private readonly IValidator<TRequest> _validator;
        public ValidatorBehavior(IValidator<TRequest> validator) => _validator = validator; 

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Skip pipeline once the request name is not ends with Command
            if (CommandExtension.IsNotCommand<TRequest>()) return await next();

            // Validate request
            ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return (TResponse)Result.Failure(CommonError.ValidationFailure<CreateDriverCommand>(validationResult.Errors));

            return await next();
        }
    }
}
