using AutoMapper;
using BTS.Core.Commands.Models.Bus;
using BTS.Core.Results;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Entities;
using MediatR;

namespace BTS.Core.Commands.Handlers
{
    public sealed class BusCommandHandler :
        IRequestHandler<CreateBusCommand, Result>
    {
        private readonly IBusRepository _repository;
        private readonly IMapper _mapper;
        public BusCommandHandler(IBusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateBusCommand request, CancellationToken cancellationToken)
        {
            // Convert dto to entity (prepare new bus info)
            var newBus = _mapper.Map<Bus>(request);

            // Create new bus in the database
            await _repository.CreateAsync(newBus, cancellationToken);

            return Result.Success("Bus created successfully.");
        }
    }
}
