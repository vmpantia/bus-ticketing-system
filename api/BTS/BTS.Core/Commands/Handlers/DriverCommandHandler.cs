using AutoMapper;
using BTS.Core.Commands.Models.Driver;
using BTS.Domain.Results;
using BTS.Domain.Results.Errors;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Dtos.Driver;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using MediatR;

namespace BTS.Core.Commands.Handlers
{
    public sealed class DriverCommandHandler :
        IRequestHandler<CreateDriverCommand, Result>,
        IRequestHandler<UpdateDriverCommand, Result>,
        IRequestHandler<UpdateDriverStatusCommand, Result>
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
            // Convert request to entity (prepare new driver info)
            var newDriver = _mapper.Map<Driver>(request);

            // Create new driver in the database
            await _repository.CreateAsync(newDriver, cancellationToken);

            return Result.Success(new
            {
                Message = "Driver created successfully.",
                Resource = _mapper.Map<DriverDto>(newDriver)
            });
        }

        public async Task<Result> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            // Check if the driver to update is exist in the database
            if (!_repository.IsExist(data => data.Id == request.DriverIdToUpdate, out Driver driver))
                return Result.Failure(DriverError.NotFound);

            // Convert request to entity (prepare updated driver info)
            var updatedDriver = _mapper.Map(request, driver);

            // Update driver in the database
            await _repository.UpdateAsync(updatedDriver, cancellationToken);

            return Result.Success(new
            {
                Message = "Driver updated successfully.",
                Resource = _mapper.Map<DriverDto>(updatedDriver)
            });
        }

        public async Task<Result> Handle(UpdateDriverStatusCommand request, CancellationToken cancellationToken)
        {
            // Check if the driver to update is exist in the database
            if (!_repository.IsExist(data => data.Id == request.DriverId, out Driver driver))
                return Result.Failure(DriverError.NotFound);

            // Update driver status
            driver.Status = request.NewStatus;
            if (request.NewStatus == CommonStatus.Deleted)
            {
                driver.DeletedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc();
                driver.DeletedBy = request.Requestor;
            }
            else
            {
                driver.UpdatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc();
                driver.UpdatedBy = request.Requestor;
            }

            // Update driver in the database
            await _repository.UpdateAsync(driver, cancellationToken);

            return Result.Success(new 
            {
                Message = "Driver status updated successfully.",
                Resource = _mapper.Map<DriverDto>(driver)
            });
        }
    }
}
