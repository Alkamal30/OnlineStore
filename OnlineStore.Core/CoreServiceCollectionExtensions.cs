using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Core.Abstractions.Services.Email;
using OnlineStore.Core.Abstractions.Services.RabbitMq;
using OnlineStore.Core.Abstractions.Services.StatisticsCollection;
using OnlineStore.Core.Services;
using OnlineStore.Core.Services.Authorization;
using OnlineStore.Core.Services.Crud;
using OnlineStore.Core.Services.Email;
using OnlineStore.Core.Services.RabbitMq;
using OnlineStore.Core.Services.Redis;
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