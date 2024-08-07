using BTS.Core.Commands.Models.Authentication;
using FluentValidation;

namespace BTS.Core.Validators.Authentication
{
    public class ResetPasswordByEmailCommandValidator : AbstractValidator<ResetPasswordByEmailCommand>
    {
        public ResetPasswordByEmailCommandValidator()
        {
            RuleFor(property => property.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();
        }
    }
}
