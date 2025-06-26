using System.Linq.Expressions;

namespace GetMyTicket.Persistance.Generic_Repository
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        public Task AddAsync(T entity, CancellationToken cancellationToken = default);

        public void Update(T entity);

        public void Delete(T entity);

        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null,
          bool AsNonTracking = false,
          CancellationToken cancellationToken = default,
          params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllAsync(
          Expression<Func<T, bool>>? filter = null,
          Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
          bool AsNonTracking = false,
          CancellationToken cancellationToken = default,
          params Expression<Func<T, object>>[] includes
        );
    }
}
