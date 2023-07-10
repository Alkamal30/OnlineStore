using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Core.Abstractions.Services.Redis;

namespace OnlineStore.Core.Services.Redis;

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