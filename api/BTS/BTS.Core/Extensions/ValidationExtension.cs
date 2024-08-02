using BTS.Domain.Models.Errors;
using FluentValidation.Results;

namespace BTS.Core.Extensions
{
    public static class ValidationExtension
    {
        public static IEnumerable<PropertyError> ConvertToPropertyErrors(this List<ValidationFailure> failures) =>
                failures.GroupBy(data => data.PropertyName)
                        .Select(data => new PropertyError
                        {
                            Property = data.Key,
                            Messages = data.Select(data => data.ErrorMessage)
                        });
    }
}
