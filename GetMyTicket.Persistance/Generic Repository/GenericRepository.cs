
using GetMyTicket.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace GetMyTicket.Persistance.Generic_Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public AppDbContext Context;
        public DbSet<T> DbSet;

        public GenericRepository(AppDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
          Expression<Func<T, bool>>? filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
          bool AsNonTracking = false,
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

        public async Task<T> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
    }
}
