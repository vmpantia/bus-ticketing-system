using AutoMapper;
using BTS.Core.Commands.Models;
using BTS.Core.Results;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Extensions;
using BTS.Domain.Models.Entities;
using BTS.Domain.Models.Enums;
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
    }
}
