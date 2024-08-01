using AutoMapper;
using BTS.Core.Commands.Models.Bus;
using BTS.Core.Results;
using BTS.Core.Results.Errors;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using MediatR;

namespace BTS.Core.Commands.Handlers
{
    public sealed class BusCommandHandler :
        IRequestHandler<CreateBusCommand, Result>,
        IRequestHandler<UpdateBusCommand, Result>,
        IRequestHandler<UpdateBusStatusCommand, Result>
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

        public async Task<Result> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
        {
            // Check if the bus to update is exist in the database
            if (!_repository.IsExist(data => data.Id == request.BusIdToUpdate, out Bus bus))
                return Result.Failure(BusError.NotFound);

            // Convert dto to entity (prepare updated driver info)
            var updatedBus = _mapper.Map(request, bus);

            // Update driver in the database
            await _repository.UpdateAsync(updatedBus, cancellationToken);

            return Result.Success("Bus updated successfully.");
        }

        public async Task<Result> Handle(UpdateBusStatusCommand request, CancellationToken cancellationToken)
        {
            // Check if the bus to update is exist in the database
            if (!_repository.IsExist(data => data.Id == request.DriverId, out Bus bus))
                return Result.Failure(BusError.NotFound);

            // Update driver status
            bus.Status = request.NewStatus;
            if (request.NewStatus == CommonStatus.Deleted)
            {
                bus.DeletedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc();
                bus.DeletedBy = request.Requestor;
            }
            else
            {
                bus.UpdatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc();
                bus.UpdatedBy = request.Requestor;
            }

            // Update driver in the database
            await _repository.UpdateAsync(bus, cancellationToken);

            return Result.Success("Bus status updated successfully.");
        }
    }
}
