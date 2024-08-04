using BTS.Core.Commands.Models.Authentication;
using BTS.Domain.Contractors.Authentication;
using FluentValidation;

namespace BTS.Core.Validators.Authentication
{
    public class LoginByTokenCommandValidator : AbstractValidator<LoginByTokenCommand>
    {
        private readonly IJwtProvider _jwtProvider;
        public LoginByTokenCommandValidator(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider;

            RuleFor(property => property.Token)
                .NotNull()
                .NotEmpty()
                .MustAsync(async (command, token, cancellation) =>
                {
                    var isValid = await _jwtProvider.IsTokenValid(token);
                    return isValid;
                }).WithMessage("Access Token is invalid or expired."); ;
        }
    }
}
