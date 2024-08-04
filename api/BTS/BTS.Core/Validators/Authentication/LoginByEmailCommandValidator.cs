using BTS.Core.Commands.Models.Authentication;
using FluentValidation;

namespace BTS.Core.Validators.Authentication
{
    public class LoginByEmailCommandValidator : AbstractValidator<LoginByEmailCommand>
    {
        public LoginByEmailCommandValidator()
        {
            RuleFor(property => property.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
