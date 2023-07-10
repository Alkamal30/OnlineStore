using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Core.Abstractions.Services.StatisticsCollection;

namespace OnlineStore.Core.Services;

public static class StatisticsCollectorServiceCollectionExtensions {

	private static IStatisticsCollector _statisticsCollector;

	public static IServiceCollection AddStatisticsCollector(this IServiceCollection services) {

		var servicesProvider = services.BuildServiceProvider();
		_statisticsCollector = servicesProvider.GetService<IStatisticsCollector>();

		return services;
	}

	public static IServiceProvider UseStatisticsCollector(this IServiceProvider provider) {

		RecurringJob.AddOrUpdate(
			methodCall: () => _statisticsCollector.CollectAndSendAsync(),
			cronExpression: Cron.Daily()
		);

		return provider;
	}
}
