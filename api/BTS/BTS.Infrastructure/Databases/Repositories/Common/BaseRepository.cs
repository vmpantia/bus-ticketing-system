using BTS.Domain.Contractors.Repositories.Common;
using BTS.Infrastructure.Databases.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Frozen;
using System.Linq.Expressions;

namespace BTS.Infrastructure.Databases.Repositories.Common
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly BTSDbContext _context;
        protected readonly DbSet<TEntity> _table;
        public BaseRepository(BTSDbContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public async Task<IQueryable<TEntity>> GetAllAsync(CancellationToken token) =>
            await Task.Run(() => _table, token);

        public async Task<IQueryable<TEntity>> GetByExpressionAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token) =>
            await Task.Run(() => _table.Where(expression), token);

        public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token) =>
            await _table.Where(expression).FirstAsync(token);

        public TEntity GetOne(Expression<Func<TEntity, bool>> expression) =>
            _table.Where(expression).First(expression);

        public bool IsExist(Expression<Func<TEntity, bool>> expression, out TEntity entity)
        {
            entity = _table.Where(expression).FirstOrDefault();
            return entity is not null;
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, CancellationToken token) =>
            await _table.AnyAsync(expression, token);

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken token)
        {
            await _table.AddAsync(entity, token);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token)
        {
            await Task.Run(() => _table.Update(entity), token);
            return entity;
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken token) =>
            await Task.Run(() => _table.Remove(entity), token);
    }
}
