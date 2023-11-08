
using Eccomerce.Infrastructure.Concreates.Storage;
using Eccomerce.Infrastructure.Concreates.Storage.Local;
using Eccomerce.Infrastructure.Enums;
using Ecomerce.Application.Abstractions;
using Ecomerce.Application.Abstractions.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace Eccomerce.Infrastructure.ServiceRegistration;

public static class ServiceRegistration
{
    public static void AddInfrastructureService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IStorageService, StorageService>();

    }
    public static void AddStorage<T>(this IServiceCollection serviceCollection) where T : class, IStorage
    {
        serviceCollection.AddScoped<IStorage, T>();
    }
    public static void AddStorage(this IServiceCollection serviceCollection, StorgeType storgeType)
    {
        switch (storgeType)
        {
            case StorgeType.Local:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
            case StorgeType.Azure:
                break;
            case StorgeType.AWS:
                break;
            default:
                serviceCollection.AddScoped<IStorage, LocalStorage>();
                break;
        }
    }
}