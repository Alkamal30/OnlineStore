using Microsoft.Extensions.Caching.Distributed;

namespace OnlineStore.Core.Services.RedisCache;

public interface IRedisService {

	public IDistributedCache DistributedCache { get; set; }


	T? GetObject<T>(string key);
	void SetObject<T>(string key, T value);
	
	Task<T?> GetObjectAsync<T>(string key);
	Task SetObjectAsync<T>(string key, T value);

}