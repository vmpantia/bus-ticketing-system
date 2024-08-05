﻿using BTS.Core.Commands.Models.Authentication;
using BTS.Domain.Constants;
using BTS.Domain.Contractors.Authentication;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Enums;
using FluentValidation;

namespace BTS.Core.Validators.Authentication
{
    public class LoginByTokenCommandValidator : AbstractValidator<LoginByTokenCommand>
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IAccessTokenRepository _accessTokenRepository;

        private string? _userId;
        private string? _userEmail;
        private string? _tokenType;
        public LoginByTokenCommandValidator(IJwtProvider jwtProvider, 
                                            IUserRepository userRepository,
                                            IAccessTokenRepository accessTokenRepository)
        {
            _jwtProvider = jwtProvider;
            _userRepository = userRepository;
            _accessTokenRepository = accessTokenRepository;

            RuleFor(property => property.Token)
                .NotNull()
                .NotEmpty()
                .WithName("Access Token")
                .MustAsync(async (command, token, cancellation) =>
                {
                    var isValid = await _jwtProvider.IsTokenValid(token);
                    return isValid;
                }).WithMessage("'{PropertyName}' is invalid or expired.")
                .MustAsync(async (command, token, cancellation) =>
                {
                    var isExist = await _accessTokenRepository.IsExistAsync(data => data.Token.Equals(token), 
                                                                           cancellation);
                    return isExist;
                }).WithMessage("'{PropertyName}' is not found in the database.")
                .MustAsync(async (command, token, cancellation) =>
                {
                    var isUsed = await _accessTokenRepository.IsTokenUsedAsync(token, cancellation);
                    return !isUsed;
                }).WithMessage("'{PropertyName}' is already used by the user.")
                .Must((token) =>
                {
                    _userId = _jwtProvider.GetValueByClaim(Common.CLAIM_NAME_USER_ID, token);
                    return Guid.TryParse(_userId, out Guid id);
                }).WithMessage("Id from the {PropertyName} is not a valid User Id.")
                .Must((token) =>
                {
                    _userEmail = _jwtProvider.GetValueByClaim(Common.CLAIM_NAME_USER_EMAIL, token);
                    return !string.IsNullOrEmpty(_userEmail);
                }).WithMessage("Email from the {PropertyName} must have value.")
                .Must((token) =>
                {
                    _tokenType = _jwtProvider.GetValueByClaim(Common.CLAIM_NAME_TOKEN_TYPE, token);
                    return !string.IsNullOrEmpty(_tokenType);
                }).WithMessage("Type from the {PropertyName} must have value.")
                .MustAsync(async (command, token, cancellation) =>
                {
                    if(Guid.TryParse(_userId, out Guid id))
                    {
                        // Check if the user exist in the database
                        var isExist = await _userRepository.IsExistAsync(data => data.Id == id &&
                                                                                 data.Email.Equals(_userEmail) &&
                                                                                 data.Status == CommonStatus.Active,
                                                                         cancellation);
                        return isExist;
                    }
                    return false;
                }).WithMessage("Id and Email of user from the {PropertyName} is not found in the database.");
        }
    }
}
