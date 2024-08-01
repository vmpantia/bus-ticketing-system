using BTS.Core.Commands.Models.Driver;
using BTS.Core.Validators.Driver.Common;
using BTS.Domain.Contractors.Repositories;

namespace BTS.Core.Validators.Driver
{
    public class UpdateDriverCommandValidator : CreateUpdateDriverCommandBaseValidator<UpdateDriverCommand>
    {
        public UpdateDriverCommandValidator(IDriverRepository repository) : base(repository) { }

        public async override Task<bool> IsLicenseNumberUniqueAsync(UpdateDriverCommand command, string license, CancellationToken token)
        {
            // Check if the updated license number is already used by other driver
            bool isExists = await _repository.IsExistAsync(data => data.Id != command.DriverIdToUpdate && 
                                                                   data.LicenseNo == license, token);
            return !isExists;
        }
    }
}
