using BTS.Core.Commands.Models.Bus;
using BTS.Domain.Contractors.Repositories;
using FluentValidation;

namespace BTS.Core.Validators.Bus
{
    public class UpdateBusStatusCommandValidator : AbstractValidator<UpdateBusStatusCommand>
    {
        private readonly IBusRepository _repository;
        public UpdateBusStatusCommandValidator(IBusRepository repository)
        {
            _repository = repository;

            RuleFor(property => property.NewStatus)
                .MustAsync(async (command, newStatus, cancellation) =>
                {
                    var isSame = await _repository.IsExistAsync(data => data.Id == command.BusId &&
                                                                        data.Status == newStatus, 
                                                                cancellation);
                    return !isSame;
                })
                .WithName("New Status")
                .WithMessage("'{PropertyName}' is same in the current driver status.");
        }
    }
}
