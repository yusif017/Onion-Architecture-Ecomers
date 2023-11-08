using Ecomerce.Application.Repositories;
using Ecomerce.Application.Repositories.Customers;
using Ecomerce.Application.Repositories.Orders;
using Ecomerce.Application.Repositories.Products;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence.ServiceRegistrations;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {

        #region context
        services.AddDbContext<EcommerceAPIDbContext>(options =>
        options.UseSqlServer(Configuration.ConnectionString));

        #endregion

        #region customer
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteReposiyory, CustomerWriteRepository>();
        #endregion

        #region order
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        #endregion

        #region product
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        #endregion


    }
}