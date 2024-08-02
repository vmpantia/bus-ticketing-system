using BTS.Domain.Contractors.Repositories.Common;
using BTS.Domain.Extensions;
using BTS.Domain.Results;
using MediatR;
using System.Transactions;

namespace BTS.Core.Behaviors
{
    public class UnitOfWorkPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
        where TResponse : Result
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkPipelineBehavior(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork; 

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Skip pipeline once the request name is not ends with Command
            if (CommandExtension.IsNotCommand<TRequest>()) return await next();

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
    }
}
