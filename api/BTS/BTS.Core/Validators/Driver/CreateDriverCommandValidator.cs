using BTS.Core.Commands.Models.Driver;
using BTS.Core.Validators.Driver.Common;
using BTS.Domain.Contractors.Repositories;

namespace BTS.Core.Validators.Driver
{
    public class CreateDriverCommandValidator : CreateUpdateDriverCommandBaseValidator<CreateDriverCommand>
    {
        public CreateDriverCommandValidator(IDriverRepository repository) : base(repository) { }

        public async override Task<bool> IsLicenseNumberUniqueAsync(CreateDriverCommand command, string license, CancellationToken token)
        {
            // Check if the updated license number is already used by other driver
            bool isExists = await _repository.IsExistAsync(data => data.LicenseNo == license, token);
            return !isExists;
        }
    }
}
