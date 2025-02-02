using GetMyTicket.Common.Entities;

namespace GetMyTicket.Service.Contracts
{
    public interface ICityService
    {
        /// <summary>
        /// Returns all entities of type city with the option to turn off tracking for read-only cases;
        /// </summary>
        /// <param name="asNoTracking">An optional query setting for entities not to be tracked by EF
        /// after being retrieved from db.</param>
        /// <returns></returns>
        public Task<IEnumerable<City>> GetAll(bool asNoTracking);
    }
}
