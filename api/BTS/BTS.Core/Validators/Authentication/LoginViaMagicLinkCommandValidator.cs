using BTS.Core.Commands.Models.Authentication;
using FluentValidation;

namespace BTS.Core.Validators.Authentication
{
    public class LoginViaMagicLinkCommandValidator : AbstractValidator<LoginViaMagicLinkCommand>
    {
        public LoginViaMagicLinkCommandValidator()
        {
            RuleFor(property => property.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
