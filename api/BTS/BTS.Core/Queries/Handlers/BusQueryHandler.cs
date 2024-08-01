using AutoMapper;
using BTS.Core.Results.Errors;
using BTS.Core.Results;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Entities;
using MediatR;
using System.Linq.Expressions;
using BTS.Domain.Models.Dtos.Bus;
using BTS.Core.Queries.Models.Bus;

namespace BTS.Core.Queries.Handlers
{
    public class BusQueryHandler :
        IRequestHandler<GetBusesQuery, Result>,
        IRequestHandler<GetBusByIdQuery, Result>
    {
        private readonly IBusRepository _repository;
        private readonly IMapper _mapper;
        public BusQueryHandler(IBusRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(GetBusesQuery request, CancellationToken cancellationToken)
        {
            // Get buses from the database
            var buses = await _repository.GetBusesFullInfoAsync(cancellationToken);

            // Check if the buses from database is NULL
            if (buses is null) return Result.Failure(DriverError.Null);

            // Convert drivers entity to dto
            var dto = _mapper.Map<IEnumerable<BusDto>>(buses);
            return Result.Success(dto);
        }

        public async Task<Result> Handle(GetBusByIdQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Bus, bool>> expression = data => data.Id == request.Id;

            // Check if the bus exist from the database
            if (!await _repository.IsExistAsync(expression, cancellationToken))
                return Result.Failure(DriverError.NotFound);

            // Get bus from the database
            var bus = await _repository.GetBusFullInfoAsync(expression, cancellationToken);

            // Check if the bus from database is NULL
            if (bus is null) return Result.Failure(DriverError.Null);

            // Convert bus entity to dto
            var dto = _mapper.Map<BusDto>(bus);
            return Result.Success(dto);
        }
    }
}
