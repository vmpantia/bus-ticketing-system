using AutoMapper;
using BTS.Core.Commands.Models;
using BTS.Core.Results;
using BTS.Core.Results.Errors;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using MediatR;

namespace BTS.Core.Commands.Handlers
{
    public class DriverCommandHandler :
        IRequestHandler<CreateDriverCommand, Result>,
        IRequestHandler<UpdateDriverCommand, Result>
    {
        private readonly IDriverRepository _repository;
        private readonly IMapper _mapper;
        public DriverCommandHandler(IDriverRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            // Convert dto to entity (prepare new driver info)
            var newDriver = _mapper.Map<Driver>(request);
            newDriver.Id = Guid.NewGuid();
            newDriver.Status = CommonStatus.Active;
            newDriver.CreatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc();
            newDriver.CreatedBy = request.Requestor;

            // Create new driver in the database
            await _repository.CreateAsync(newDriver, cancellationToken);

            return Result.Success($"Driver created successfully. (Id: ${newDriver.Id}");
        }

        public async Task<Result> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            // Check if the driver to update is exist in the database
            if (!_repository.IsExist(data => data.Id == request.DriverIdToUpdate, out Driver driver))
                return Result.Failure(DriverError.NotFound);

            // Convert dto to entity (prepare updated driver info)
            var updatedDriver = _mapper.Map(request, driver);
            updatedDriver.UpdatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc();
            updatedDriver.UpdatedBy = request.Requestor;

            // Create new driver in the database
            await _repository.UpdateAsync(updatedDriver, cancellationToken);

            return Result.Success($"Driver updated successfully. (Id: ${updatedDriver.Id}");
        }
    }
}
