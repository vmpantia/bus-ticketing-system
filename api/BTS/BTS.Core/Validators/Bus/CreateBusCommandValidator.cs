using BTS.Core.Commands.Models.Bus;
using BTS.Core.Validators.Bus.Common;
using BTS.Domain.Contractors.Repositories;

namespace BTS.Core.Validators.Bus
{
    public class CreateBusCommandValidator : CreateUpdateBusCommandBaseValidator<CreateBusCommand>
    {
        public CreateBusCommandValidator(IBusRepository busRepository, IDriverRepository driverRepository, IRouteRepository routeRepository) : base(busRepository, driverRepository, routeRepository) { }

        public async override Task<bool> IsPlateNumberUniqueAsync(CreateBusCommand command, string plateNo, CancellationToken token)
        {
            // Check if the new plate number is already used by other bus
            bool isExists = await _busRepository.IsExistAsync(data => data.PlateNo == plateNo, token);
            return !isExists;
        }
    }
}
