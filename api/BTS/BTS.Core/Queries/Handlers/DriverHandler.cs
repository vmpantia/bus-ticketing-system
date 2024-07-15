using AutoMapper;
using BTS.Core.Queries.Models.Driver;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Dtos.Driver;
using BTS.Domain.Results;
using BTS.Domain.Results.Errors;
using MediatR;

namespace BTS.Core.Queries.Handlers
{
    public class DriverHandler : IRequestHandler<GetDriversQuery, ApiResponse<IEnumerable<DriverDto>>>
    {
        private readonly IDriverRepository _repository;
        private readonly IMapper _mapper;

        public DriverHandler(IDriverRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<DriverDto>>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            // Get drivers from the database
            var drivers = await _repository.GetDriversFullInfoAsync(cancellationToken);

            // Check if the drivers from database is NULL
            if (drivers is null) return ApiResponse<IEnumerable<DriverDto>>.Failure(DriverError.Null);

            // Convert drivers entity to dto
            var dto = _mapper.Map<IEnumerable<DriverDto>>(drivers);
            return ApiResponse<IEnumerable<DriverDto>>.Success(dto);
        }
    }
}
