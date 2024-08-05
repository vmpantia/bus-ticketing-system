using BTS.Core.Commands.Models.User;
using BTS.Domain.Contractors.Repositories;
using FluentValidation;

namespace BTS.Core.Validators.User
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IUserRepository _repository;
        public CreateUserCommandValidator(IUserRepository repository)
        {
            _repository = repository;

            RuleFor(property => property.Username)
                .NotNull()
                .NotEmpty()
                .Length(7, 15);

            RuleFor(property => property.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(property => property.Password)
                .NotNull()
                .NotEmpty();

            RuleFor(property => property.FirstName)
                .NotNull()
                .NotEmpty()
                .WithName("First Name");

            RuleFor(property => property.LastName)
                .NotNull()
                .NotEmpty()
                .WithName("Last Name");
        }
    }
}
