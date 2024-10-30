namespace EventsWebApplication.Domain.Interfaces
{
    public interface ICacheRepository
    {
        public Task SetAsync<TObject>(string key, TObject value, TimeSpan expiresIn);
        public Task<TObject?> GetAsync<TObject>(string key);
        public Task DeleteAsync<TObject>(string key);
    }
}