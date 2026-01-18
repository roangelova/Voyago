using GetMyTicket.Common.Entities;
using GetMyTicket.Persistance.Context;
using GetMyTicket.Persistance.Filters;
using GetMyTicket.Persistance.Generic_Repository;
using GetMyTicket.Persistance.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace GetMyTicket.Persistance.Repositories
{
    public class TripRepository : GenericRepository<Trip>, ITripRepository
    {
        public TripRepository(AppDbContext context)
             : base(context)
        {
        }

        public override Task<IEnumerable<Trip>> GetAllAsync(BaseFilter filter, CancellationToken cancellationToken = default)
        {
            //TODO --> IMPLEMENT THIS 
            return base.GetAllAsync(filter, cancellationToken);
        }
    }
}
