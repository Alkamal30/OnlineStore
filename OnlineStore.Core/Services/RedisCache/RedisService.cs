using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace OnlineStore.Core.Services.RedisCache;

public class RedisService : IRedisService {
		
	public RedisService(IDistributedCache cache) {
		_cache = cache;
	}


	private IDistributedCache _cache;



	public IDistributedCache DistributedCache {
		get => _cache;
		set => _cache = value;
	}


	public T? GetObject<T>(string key) {
		var jsonValue = _cache.GetString(key);

		if(jsonValue is null)
			return default(T);

		return JsonSerializer.Deserialize<T>(jsonValue);
	}

	public void SetObject<T>(string key, T value) {
		var jsonValue = JsonSerializer.Serialize(value);

		_cache.SetString(key, jsonValue);
	}


	public async Task<T?> GetObjectAsync<T>(string key) {
		var jsonValue = await _cache.GetStringAsync(key);

		if(jsonValue is null)
			return default(T);

		return JsonSerializer.Deserialize<T>(jsonValue);
	}

	public async Task SetObjectAsync<T>(string key, T value) {
		var jsonValue = JsonSerializer.Serialize(value);

		await _cache.SetStringAsync(key, jsonValue);
	}
}