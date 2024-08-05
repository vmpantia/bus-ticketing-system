using BTS.Core.Commands.Models.Authentication;
using FluentValidation;

namespace BTS.Core.Validators.Authentication
{
    public class PasswordResetByEmailCommandValidator : AbstractValidator<PasswordResetByEmailCommand>
    {
        public PasswordResetByEmailCommandValidator()
        {
            RuleFor(property => property.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
