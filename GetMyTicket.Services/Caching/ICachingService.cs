namespace GetMyTicket.Service.Caching
{
    public interface ICachingService
    {
        Task<T> Get<T>(string key);
        Task<T> Set<T>(string key, T value, TimeSpan? expiresAt = null);
        Task<T> Update<T>(string key, T value, TimeSpan? expiresAt = null);
        Task Remove(string key);
    }
}
