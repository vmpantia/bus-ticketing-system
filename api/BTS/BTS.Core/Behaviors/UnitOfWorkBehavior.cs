using BTS.Domain.Contractors.Repositories.Common;
using MediatR;
using System.Transactions;

namespace BTS.Core.Behaviors
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkBehavior(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork; 

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Skip pipeline once the request name is not ends with Command
            if (IsNotCommand()) return await next();

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Process the next pipeline
                var response = await next();

                // Save all the changes
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                // Complete all the transaction
                transactionScope.Complete();

                return response;
            }
        }

        private static bool IsNotCommand() => !typeof(TRequest).Name.EndsWith("Command");
    }
}
