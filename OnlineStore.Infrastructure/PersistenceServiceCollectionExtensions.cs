using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineStore.Infrastructure;

public static class PersistenceServiceCollectionExtensions {

	public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration) {

		services.AddDbContext<OnlineStoreDbContext>(
			options => options.UseNpgsql(
				configuration.GetConnectionString("Default")	
			)
		);

		return services;
	}
}