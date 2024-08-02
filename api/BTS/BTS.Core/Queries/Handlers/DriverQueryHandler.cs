using AutoMapper;
using BTS.Core.Queries.Models.Driver;
using BTS.Domain.Results;
using BTS.Domain.Results.Errors;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Dtos.Driver;
using BTS.Domain.Models.Entities;
using MediatR;
using System.Linq.Expressions;

namespace BTS.Core.Queries.Handlers
{
    public sealed class DriverQueryHandler : 
        IRequestHandler<GetDriversQuery, Result>,
        IRequestHandler<GetDriverByIdQuery, Result>
    {
        private readonly IDriverRepository _repository;
        private readonly IMapper _mapper;
        public DriverQueryHandler(IDriverRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            // Get drivers from the database
            var drivers = await _repository.GetDriversFullInfoAsync(cancellationToken);

            // Check if the drivers from database is NULL
            if (drivers is null) return Result.Failure(DriverError.Null);

            // Convert drivers entity to dto
            var dto = _mapper.Map<IEnumerable<DriverDto>>(drivers);
            return Result.Success(dto);
        }

        public async Task<Result> Handle(GetDriverByIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Driver, bool>> expression = data => data.Id == request.Id;

            // Check if the driver exist from the database
            if (!await _repository.IsExistAsync(expression, cancellationToken)) 
                return Result.Failure(DriverError.NotFound);

            // Get driver from the database
            var driver = await _repository.GetDriverFullInfoAsync(expression, cancellationToken);

            // Check if the driver from database is NULL
            if (driver is null) return Result.Failure(DriverError.Null);

            // Convert driver entity to dto
            var dto = _mapper.Map<DriverDto>(driver);
            return Result.Success(dto);
        }
    }
}
