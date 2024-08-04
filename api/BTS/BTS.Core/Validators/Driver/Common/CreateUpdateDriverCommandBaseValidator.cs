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
                .NotEmpty()
                .WithName("License Number")
                .MustAsync(async (command, license, cancellation) =>
                {
                    var isUnique = await IsLicenseNumberUniqueAsync(command, license, cancellation);
                    return isUnique;
                }).WithMessage(ErrorMessage.ERROR_UNIQUE_VALUE_MESSAGE);

            RuleFor(property => property.FirstName)
                .NotNull()
                .NotEmpty()
                .WithName("First Name");

            RuleFor(property => property.LastName)
                .NotNull()
                .NotEmpty()
                .WithName("Last Name");

            RuleFor(property => property.Gender)
                .NotNull()
                .Must(gender => new[] { "Male", "Female", "Other" }.Contains(gender))
                    .WithMessage("'{PropertyName}' must be 'Male', 'Female', or 'Other'.");

            RuleFor(property => property.Address)
                .NotNull()
                .NotEmpty();

            RuleFor(property => property.ContactNo)
                .NotNull()
                .NotEmpty()
                .WithName("Contact Number");

            RuleFor(property => property.Birthdate)
                .NotNull()
                .NotEmpty()
                .Must(birthDate => DateTimeExtension.IsFutureDate(birthDate))
                    .WithMessage("'{PropertyName}' must not be a future date.");
        }

        public abstract Task<bool> IsLicenseNumberUniqueAsync(TCommand command, string license, CancellationToken token);
    }
}
