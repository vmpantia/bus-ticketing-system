using BTS.Core.Commands.Models.Driver;
using BTS.Domain.Contractors.Repositories;
using FluentValidation;

namespace BTS.Core.Validators.Driver
{
    public class UpdateDriverStatusCommandValidator : AbstractValidator<UpdateDriverStatusCommand>
    {
        private readonly IDriverRepository _repository;
        public UpdateDriverStatusCommandValidator(IDriverRepository repository)
        {
            _repository = repository;

            RuleFor(property => property.NewStatus)
                .MustAsync(async (command, newStatus, cancellation) =>
                {
                    var isSame = await _repository.IsExistAsync(data => data.Id == command.DriverId &&
                                                                        data.Status == newStatus, 
                                                                cancellation);
                    return !isSame;
                })
                .WithMessage("Current status is same with the new status.");
        }
    }
}
