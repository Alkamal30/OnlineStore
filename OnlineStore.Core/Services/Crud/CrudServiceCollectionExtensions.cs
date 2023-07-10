using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Core.Abstractions.Services.Crud;

namespace OnlineStore.Core.Services.Crud;

public static class CrudServiceCollectionExtensions
{

    public static IServiceCollection AddCrud(this IServiceCollection services)
    {
        services
            .AddScoped<IUserCrudService, UserCrudService>()
            .AddScoped<IProductCrudService, ProductCrudService>()
            .AddScoped<IOrderCrudService, OrderCrudService>()
            .AddScoped<IUserRequestsCrudService, UserRequestsCrudService>();

        return services;
    }

}