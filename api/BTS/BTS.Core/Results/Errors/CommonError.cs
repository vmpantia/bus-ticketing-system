using BTS.Domain.Models.Enums;
using FluentValidation.Results;

namespace BTS.Core.Results.Errors
{
    public class CommonError
    {
        public static Error Unexpected(Exception exception) => new(ErrorType.Unexpected, nameof(exception), exception.ToString());
        public static Error ValidationFailure<TRequest>(List<ValidationFailure> validationFailures) =>
            new(ErrorType.ValidationFailure,
                typeof(TRequest).Name,
                validationFailures.GroupBy(data => data.PropertyName)
                                  .Select(data => new { 
                                      property = data.Key,
                                      messages = data.Select(data => data.ErrorMessage)
                                  }));
    }
}
