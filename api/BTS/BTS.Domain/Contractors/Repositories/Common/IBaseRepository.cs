using System.Linq.Expressions;

namespace BTS.Domain.Contractors.Repositories.Common
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllAsync(CancellationToken token);
        Task<IQueryable<TEntity>> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token);
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token);
        TEntity GetOne(Expression<Func<TEntity, bool>> expression);
        bool IsExist(Expression<Func<TEntity, bool>> expression, out TEntity entity);
        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token);
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken token);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token);
        Task DeleteAsync(TEntity entity, CancellationToken token);
    }
}
