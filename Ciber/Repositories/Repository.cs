using Ciber.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ciber.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext DbContext { get; }

        protected DbSet<TEntity> DbSet { get; }

        public Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;

            DbSet = DbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var res = await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

            if (autoSave)
                await DbContext.SaveChangesAsync(cancellationToken);

            return res.Entity;
        }

        public async Task<TEntity> GetByIdAsync(object? keyValues, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>().FindAsync(keyValues);
        }

        public async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbContext.Set<TEntity>().Remove(entity);

            if (autoSave)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<TEntity>> GetAllAsync(bool isNoTracking = false, CancellationToken cancellationToken = default)
        {
            var query = GetDbSet();

            if (isNoTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbContext.Set<TEntity>().Update(entity);

            if (autoSave)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>().AnyAsync(predicate, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

            if (autoSave)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbContext.Set<TEntity>().UpdateRange(entities);

            if (autoSave)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entities = await DbContext.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);

            DbContext.Set<TEntity>().RemoveRange(entities);

            if (autoSave)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<TEntity> GetDbSet() => DbSet;

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbContext.Set<TEntity>().AsNoTracking().CountAsync(predicate, cancellationToken);
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbContext.Set<TEntity>().RemoveRange(entities);

            if (autoSave)
                await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public IQueryable<TEntity> WhereIf(bool condition, Expression<Func<TEntity, bool>> predicate)
        {
            return condition ? DbSet.Where(predicate) : DbSet;
        }
    }
}
