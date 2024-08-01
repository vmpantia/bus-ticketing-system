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
                .WithMessage((property) => $"Driver with an Id of {property.DriverId} is not found in the database, Only active and existing driver can be link to a bus.");
        
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
                .WithMessage((property) => $"Route with an Id of {property.RouteId} is not found in the database, Only active and existing route can be link to a bus.");

            RuleFor(property => property.BusNo)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "Bus number"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "Bus number"));

            RuleFor(property => property.PlateNo)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "Plate number"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "Plate number"))
                .MustAsync(async (command, plateNo, cancellation) =>
                {
                    var isUnique = await IsPlateNumberUniqueAsync(command, plateNo, cancellation);
                    return isUnique;
                })
                .WithMessage(string.Format(ErrorMessage.ERROR_UNIQUE_VALUE_FORMAT, "Plate number"));

            RuleFor(property => property.Make)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "Make"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "Make"));

            RuleFor(property => property.Model)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "Model"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "Model"));

            RuleFor(property => property.Year)
                .NotNull()
                    .WithMessage(string.Format(ErrorMessage.ERROR_NULL_VALUE_FORMAT, "Year"))
                .NotEmpty()
                    .WithMessage(string.Format(ErrorMessage.ERROR_EMPTY_VALUE_FORMAT, "Year"));
        }

        public abstract Task<bool> IsPlateNumberUniqueAsync(TCommand command, string plateNo, CancellationToken token);
    }
}
