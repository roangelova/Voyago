
using GetMyTicket.Persistance.Context;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
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
