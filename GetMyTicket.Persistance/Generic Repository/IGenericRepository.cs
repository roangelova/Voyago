using System.Linq.Expressions;

namespace GetMyTicket.Persistance.Generic_Repository
{
    public interface IGenericRepository<T> where T : class
    {
        //getById , ADD, DELETEBYID, DELETE ENTITY, UPDATE, GETALL

        public Task<T> GetByIdAsync(object id);

        public Task AddAsync(T entity);

        public void Update(T entity);

        public void Delete(T entity);

        Task<IEnumerable<T>> GetAllAsync(
          Expression<Func<T, bool>>? filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
          bool AsNonTracking = false,
          params Expression<Func<T, object>>[] includes 
   );
    }
}
