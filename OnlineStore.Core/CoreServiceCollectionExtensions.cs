using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Core.Services;
using OnlineStore.Core.Services.Authorization;
using OnlineStore.Core.Services.Crud;
using OnlineStore.Core.Services.RabbitMq;
using OnlineStore.Core.Services.RedisCache;
using OnlineStore.Infrastructure;

namespace OnlineStore.Core;

public static class CoreServiceCollectionExtensions {

	public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration) {

		services
			.AddJwtAuthentication(configuration)
			.AddAuthorization()
			.AddPersistence(configuration)
			.AddRedis(configuration)
			.AddCrud()
			.AddScoped<IEmailService, EmailService>()
			.AddScoped<IStatisticsCollector, StatisticsCollector>()
			.AddScoped<IRabbitMqService, RabbitMqService>()
			.AddAuthorizationService();

		return services;
	}
}