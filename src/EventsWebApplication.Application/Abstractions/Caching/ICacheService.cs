namespace EventsWebApplication.Application.Abstractions.Caching
{
    public interface ICacheService
    {
        Task SetAsync<TObject>(string key, TObject value);
        Task<TObject?> GetAsync<TObject>(string key);
        Task DeleteAsync<TObject>(string key);
    }
}