namespace Library_Infra.Redis
{
    public interface ICachingService
    {
        Task SetAsync(string key, string value);
        Task<string> GetAsync<T>(string key);
    }
}
