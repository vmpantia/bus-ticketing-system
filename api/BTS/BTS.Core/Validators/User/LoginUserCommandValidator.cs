using BTS.Core.Commands.Models.User;
using BTS.Domain.Contractors.Repositories;
using FluentValidation;

namespace BTS.Core.Validators.User
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        private readonly IUserRepository _repository;
        public LoginUserCommandValidator(IUserRepository repository)
        {
            _repository = repository;

            RuleFor(property => property.UsernameOrEmail)
                .NotNull()
                .NotEmpty()
                .WithName("Username or Email")
                .MustAsync(async (command, userNameOrEmail, cancellation) =>
                {
                    var isExist = await _repository.IsExistAsync(data => data.Username.Equals(userNameOrEmail) ||
                                                                         data.Email.Equals(userNameOrEmail),
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
                                                                         data.Password.Equals(password),
                                                                 cancellation);
                    return isExist;
                }).WithMessage("'{PropertyName}' is incorrect or not matched in the database.");
        }
    }
}
