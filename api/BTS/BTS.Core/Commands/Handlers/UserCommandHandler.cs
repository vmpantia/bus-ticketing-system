using AutoMapper;
using BTS.Core.Commands.Models.User;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Contractors.Services;
using BTS.Domain.Models.Dtos.User;
using BTS.Domain.Models.Entities;
using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Handlers
{
    public class UserCommandHandler :
        IRequestHandler<LoginUserCommand, Result>,
        IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        public UserCommandHandler(IUserRepository userRepository, IAuthenticationService authenticationService, IMapper mapper)
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken) =>
            await Task.Run(() =>
            {    
                var token = _authenticationService.Authenticate(request.UsernameOrEmail, request.Password, out User user);
                return Result.Success(new
                {
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Token = token
                });
            });

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Convert request to new user entity
            var newUser = _mapper.Map<User>(request);

            // Create new user in the database
            await _userRepository.CreateAsync(newUser, cancellationToken);

            return Result.Success(new
            {
                Message = "User admin created successfully.",
                Resource = _mapper.Map<UserDto>(newUser)
            });
        }
    }
}
