using AutoMapper;
using BTS.Core.Commands.Models.User;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Dtos.User;
using BTS.Domain.Models.Entities;
using BTS.Domain.Results;
using MediatR;

namespace BTS.Core.Commands.Handlers
{
    public class UserCommandHandler :
        IRequestHandler<CreateUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

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
