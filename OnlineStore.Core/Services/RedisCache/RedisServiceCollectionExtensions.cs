using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineStore.Core.Services.RedisCache;

public static class RedisServiceCollectionExtensions {

	public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration) {
		services.AddStackExchangeRedisCache(options => {
			options.Configuration = configuration["RedisSettings:Host"];
			options.InstanceName = configuration["RedisSettings:InstanceName"];
		});

		services.AddScoped<IRedisService, RedisService>();

		return services;
	}

}