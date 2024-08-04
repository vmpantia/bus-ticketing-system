using BTS.Core.Commands.Models.Authentication;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Enums;
using FluentValidation;

namespace BTS.Core.Validators.Authentication
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        private readonly IUserRepository _repository;
        public LoginCommandValidator(IUserRepository repository)
        {
            _repository = repository;

            RuleFor(property => property.UsernameOrEmail)
                .NotNull()
                .NotEmpty()
                .WithName("Username or Email")
                .MustAsync(async (command, userNameOrEmail, cancellation) =>
                {
                    var isExist = await _repository.IsExistAsync(data => (data.Username.Equals(userNameOrEmail) ||
                                                                          data.Email.Equals(userNameOrEmail)) &&
                                                                         data.Status == CommonStatus.Active,
                                                                 cancellation);
                    return isExist;
                }).WithMessage("'{PropertyName}' is not found in the database.");

            RuleFor(property => property.Password)
                .NotNull()
                .NotEmpty()
                .Length(7, 20)
                .MustAsync(async (command, password, cancellation) =>
                {
                    var isExist = await _repository.IsExistAsync(data => (data.Username.Equals(command.UsernameOrEmail) ||
                                                                          data.Email.Equals(command.UsernameOrEmail)) &&
                                                                         data.Password.Equals(password) &&
                                                                         data.Status == CommonStatus.Active,
                                                                 cancellation);
                    return isExist;
                }).WithMessage("'{PropertyName}' is incorrect or not matched in the database.");
        }
    }
}
