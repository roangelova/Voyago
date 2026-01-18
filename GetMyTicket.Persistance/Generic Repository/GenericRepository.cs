
using GetMyTicket.Common.Entities.Trackable;
using GetMyTicket.Persistance.Context;
using GetMyTicket.Persistance.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GetMyTicket.Persistance.Generic_Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, ITrackableEntity
    {
        public AppDbContext Context;
        public DbSet<T> DbSet;

        public GenericRepository(AppDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool AsNonTracking = false, CancellationToken cancellationToken = default, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (AsNonTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
          Expression<Func<T, bool>>? filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
          bool AsNonTracking = false,
          CancellationToken cancellationToken = default,
          params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = DbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (AsNonTracking)
            {
                query = query.AsNoTracking();
            }


            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(id, cancellationToken);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(BaseFilter filter, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = DbSet;

            if (filter.Page.HasValue && filter.PageSize.HasValue)
            {
                return await query
                               .Where(T => T.IsActive == filter.IsDeleted)
                               .Skip((filter.Page.Value - 1) * filter.PageSize.Value)
                               .Take(filter.PageSize.Value)
                               .ToListAsync(cancellationToken);
            }
            else
            {
                return await query
               .Where(T => T.IsActive == filter.IsDeleted)
               .ToListAsync();
            }
        }
    }
}
