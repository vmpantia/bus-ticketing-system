using BTS.Core.Commands.Models.Bus.Common;
using BTS.Domain.Constants;
using BTS.Domain.Contractors.Repositories;
using BTS.Domain.Models.Enums;
using FluentValidation;

namespace BTS.Core.Validators.Bus.Common
{
    public abstract class CreateUpdateBusCommandBaseValidator<TCommand> : AbstractValidator<TCommand>
        where TCommand : ICreateUpdateBusCommand
    {
        protected readonly IBusRepository _busRepository;
        protected readonly IDriverRepository _driverRepository;
        protected readonly IRouteRepository _routeRepository;
        public CreateUpdateBusCommandBaseValidator(IBusRepository busRepository, IDriverRepository driverRepository, IRouteRepository routeRepository)
        {
            // Repository
            _busRepository = busRepository;
            _driverRepository = driverRepository;
            _routeRepository = routeRepository;

            // Rules
            RuleFor(property => property.DriverId)
                .MustAsync(async (driverId, cancellation) =>
                {
                    // Skip when driverId is NULL
                    if (driverId is null) return true;

                    // Check if the driver id exist and active in the database
                    var isExist = await _driverRepository.IsExistAsync(data => data.Id == driverId &&
                                                                               data.Status == CommonStatus.Active,
                                                                       cancellation);
                    return isExist;
                })
                .WithName("Driver Id")
                .WithMessage((property) => "'{PropertyName}' " + $"{property.DriverId} is not found in the database, Only active and existing driver can be link to a bus.");
        
            RuleFor(property => property.RouteId)
                .MustAsync(async (routeId, cancellation) =>
                {
                    // Skip when routeId is NULL
                    if (routeId is null) return true;

                    // Check if the route id exist and active in the database
                    var isExist = await _routeRepository.IsExistAsync(data => data.Id == routeId &&
                                                                               data.Status == CommonStatus.Active,
                                                                      cancellation);
                    return isExist;
                })
                .WithMessage((property) => "'{PropertyName}' " + $"{property.DriverId} is not found in the database, Only active and existing route can be link to a bus.");

            RuleFor(property => property.BusNo)
                .NotNull()
                .NotEmpty()
                .WithName("Bus Number");

            RuleFor(property => property.PlateNo)
                .NotNull()
                .NotEmpty()
                .WithName("Plate Number")
                .MustAsync(async (command, plateNo, cancellation) =>
                {
                    var isUnique = await IsPlateNumberUniqueAsync(command, plateNo, cancellation);
                    return isUnique;
                }).WithMessage(ErrorMessage.ERROR_UNIQUE_VALUE_MESSAGE);

            RuleFor(property => property.Make)
                .NotNull()
                .NotEmpty();

            RuleFor(property => property.Model)
                .NotNull()
                .NotEmpty();

            RuleFor(property => property.Year)
                .NotNull()
                .NotEmpty();
        }

        public abstract Task<bool> IsPlateNumberUniqueAsync(TCommand command, string plateNo, CancellationToken token);
    }
}
