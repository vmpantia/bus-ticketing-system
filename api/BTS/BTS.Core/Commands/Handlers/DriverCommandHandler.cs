using AutoMapper;
using BTS.Core.Commands.Models;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
using BTS.Domain.Results;
using BTS.Domain.Results.Errors;
using MediatR;

namespace BTS.Core.Commands.Handlers
{
    public class DriverCommandHandler :
        IRequestHandler<CreateDriverCommand, Result>
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
            // Check if the license no already exist or duplicate
            if (await _repository.IsExistAsync(data => data.LicenseNo.Equals(request.LicenseNo), cancellationToken))
                return Result.Failure(DriverError.DuplicateDriversLicenseNo);

            // Convert dto to entity (prepare new driver info)
            var newDriver = _mapper.Map<Driver>(request);
            newDriver.Id = Guid.NewGuid();
            newDriver.Status = CommonStatus.Active;
            newDriver.CreatedAt = DateTimeExtension.GetCurrentDateTimeOffsetUtc();
            newDriver.CreatedBy = request.Requestor;

            // Save new driver in the database
            await _repository.CreateAsync(newDriver, cancellationToken);
            await _repository.SaveAsync(cancellationToken);

            return Result.Success($"Driver created successfully. (Id: ${newDriver.Id}");
        }
    }
}
