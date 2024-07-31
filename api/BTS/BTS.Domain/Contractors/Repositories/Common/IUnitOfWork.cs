namespace BTS.Domain.Contractors.Repositories.Common
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken token);
    }
}