using BTS.Core.Commands.Models.Driver.Common;
using BTS.Domain.Constants;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using FluentValidation;

namespace BTS.Core.Validators.Driver.Common
{
    public abstract class CreateUpdateDriverCommandBaseValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : ICreateUpdateDriverCommand
    {
        protected readonly IDriverRepository _repository;
        public CreateUpdateDriverCommandBaseValidator(IDriverRepository repository)
        {
            // Repository
            _repository = repository;

            // Rules
            RuleFor(property => property.LicenseNo)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "License number"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "License number"))
                .MustAsync(async (command, license, cancellation) =>
                {
                    var isUnique = await IsLicenseNumberUniqueAsync(command, license, cancellation);
                    return isUnique;
                })
                .WithMessage(string.Format(ErrorMessage.ERROR_UNIQUE_VALUE_FORMAT, "License number"));

            RuleFor(property => property.FirstName)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "First name"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "First name"));

            RuleFor(property => property.LastName)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "Last name"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "Last name"));

            RuleFor(property => property.Gender)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "Gender"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "Gender"))
                .Must(gender => new[] { "Male", "Female", "Other" }.Contains(gender))
                    .WithMessage("Gender must be 'Male', 'Female', or 'Other'.");

            RuleFor(property => property.Address)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "Address"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "Address"));

            RuleFor(property => property.ContactNo)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "Contact number"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "Contact number"));

            RuleFor(property => property.Birthdate)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "Birthdate"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "Birthdate"))
                .Must(birthDate => DateTimeExtension.IsFutureDate(birthDate))
                    .WithMessage("Birthdate must not be a future date.");
        }

        public abstract Task<bool> IsLicenseNumberUniqueAsync(TCommand command, string license, CancellationToken token);
    }
}
