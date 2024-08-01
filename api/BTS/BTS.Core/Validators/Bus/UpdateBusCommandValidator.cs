using BTS.Core.Commands.Models.Bus;
using BTS.Core.Validators.Bus.Common;
using BTS.Core.Validators.Driver.Common;
using BTS.Domain.Contractors.Repositories;

namespace BTS.Core.Validators.Bus
{
    public class UpdateBusCommandValidator : CreateUpdateBusCommandBaseValidator<UpdateBusCommand>
    {
        public UpdateBusCommandValidator(IBusRepository busRepository, IDriverRepository driverRepository, IRouteRepository routeRepository) : base(busRepository, driverRepository, routeRepository) { }

        public async override Task<bool> IsPlateNumberUniqueAsync(UpdateBusCommand command, string plateNo, CancellationToken token)
        {
            // Check if the updated plate number is already used by other bus
            bool isExists = await _busRepository.IsExistAsync(data => data.Id != command.BusIdToUpdate && 
                                                                      data.PlateNo == plateNo, token);
            return !isExists;
        }
    }
}
