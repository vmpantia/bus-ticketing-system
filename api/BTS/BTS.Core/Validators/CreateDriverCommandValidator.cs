using BTS.Core.Commands.Models;
using BTS.Domain.Constants;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using FluentValidation;

namespace BTS.Core.Validators
{
    public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
    {
        private readonly IDriverRepository _repository;
        public CreateDriverCommandValidator(IDriverRepository repository)
        {
            // Repository
            _repository = repository;

            // Rules
            RuleFor(property => property.LicenseNo)
                .NotNull()
                    .WithMessage(string.Format("License number", ErrorMessage.ERROR_NULL_VALUE_FORMAT))
                .NotEmpty()
                    .WithMessage(string.Format("License number", ErrorMessage.ERROR_EMPTY_VALUE_FORMAT))
                .MustAsync(async (license, cancellation) =>
                    {
                        bool isExists = await _repository.IsExistAsync(data => data.LicenseNo == license, cancellation);
                        return !isExists;
                    }).WithMessage(string.Format("License number", ErrorMessage.ERROR_UNIQUE_VALUE_FORMAT));

            RuleFor(property => property.FirstName)
                .NotNull()
                    .WithMessage(string.Format("First name", ErrorMessage.ERROR_NULL_VALUE_FORMAT))
                .NotEmpty()
                    .WithMessage(string.Format("First name", ErrorMessage.ERROR_EMPTY_VALUE_FORMAT));

            RuleFor(property => property.LastName)
                .NotNull()
                    .WithMessage(string.Format("Last name", ErrorMessage.ERROR_NULL_VALUE_FORMAT))
                .NotEmpty()
                    .WithMessage(string.Format("Last name", ErrorMessage.ERROR_EMPTY_VALUE_FORMAT));

            RuleFor(property => property.Gender)
                .NotNull()
                    .WithMessage(string.Format("Gender", ErrorMessage.ERROR_NULL_VALUE_FORMAT))
                .NotEmpty()
                    .WithMessage(string.Format("Gender", ErrorMessage.ERROR_EMPTY_VALUE_FORMAT))
                .Must(gender => new[] { "Male", "Female", "Other" }.Contains(gender))
                    .WithMessage("Gender must be 'Male', 'Female', or 'Other'");

            RuleFor(property => property.Address)
                .NotNull()
                    .WithMessage(string.Format("Address", ErrorMessage.ERROR_NULL_VALUE_FORMAT))
                .NotEmpty()
                    .WithMessage(string.Format("Address", ErrorMessage.ERROR_EMPTY_VALUE_FORMAT));

            RuleFor(property => property.ContactNo)
                .NotNull()
                    .WithMessage(string.Format("Contact number", ErrorMessage.ERROR_NULL_VALUE_FORMAT))
                .NotEmpty()
                    .WithMessage(string.Format("Contact number", ErrorMessage.ERROR_EMPTY_VALUE_FORMAT));

            RuleFor(property => property.Birthdate)
                .NotNull()
                    .WithMessage(string.Format("Birthdate", ErrorMessage.ERROR_NULL_VALUE_FORMAT))
                .NotEmpty()
                    .WithMessage(string.Format("Birthdate", ErrorMessage.ERROR_EMPTY_VALUE_FORMAT))
                .Must(birthDate => DateTimeExtension.IsFutureDate(birthDate))
                    .WithMessage("Birthdate must not be a future date.");
        }
    }
}
