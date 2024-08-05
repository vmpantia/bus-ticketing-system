using BTS.Core.Commands.Models.Authentication;
using BTS.Domain.Contractors.Repositories;
using FluentValidation;

namespace BTS.Core.Validators.Authentication
{
    public class LoginByEmailCommandValidator : AbstractValidator<LoginByEmailCommand>
    {
        private readonly IUserRepository _userRepository;
        public LoginByEmailCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(property => property.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
