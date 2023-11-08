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

        #region Context
        services.AddDbContext<EcommerceAPIDbContext>(options =>
        options.UseSqlServer(Configuration.ConnectionString));

        #endregion

        #region Customer
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteReposiyory, CustomerWriteRepository>();
        #endregion

        #region Order
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        #endregion

        #region Product
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        #endregion

        #region File
        services.AddScoped<IFileReadRepsoitory, FileReadRepsoitory>();
        services.AddScoped<IFileWriteRepository, FileWriteRepository>();
        #endregion

        #region InvoiceFile
        services.AddScoped<IInvoiceFileReadRepsoitory, InvoiceFileReadRepsoitory>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
        #endregion

        #region ProductImageFile
        services.AddScoped<IProductImageFileReadRepsoitory, ProductImageFileReadRepsoitory>();
        services.AddScoped<IProductImageFileReadRepsoitory, ProductImageFileReadRepsoitory>();
        #endregion
    }
}