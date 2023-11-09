using Ecomerce.Application.Repositories;
using Ecomerce.Application.Repositories.Customers;
using Ecomerce.Application.Repositories.Orders;
using Ecomerce.Application.Repositories.Products;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Application.Repositories.ProductImageFileRepositories;
using Ecomerce.Application.Repositories.FileRepositories;
using Ecomerce.Application.Repositories.InvoiceFileRepositories;
using Ecomerce.Application.Repositories.ProductImageFileRepositories;
using Ecommerce.Persistence.Repositories.FileRepositories;
using Ecommerce.Persistence.Repositories.InvoiceFileRepositories;
using Ecommerce.Persistence.Repositories.ProductImageFileRepositories;

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



        services.AddScoped<IFileReadRepositories, FileReadRepositories>();
        services.AddScoped<IFileWriteRepositories, FileWriteRepositories>();

        services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
        services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

        services.AddScoped<IProductImageFileReadRepository, ProductImageReadFileRepository>();
        services.AddScoped<IProductImageFileWriteRepository, ProductImageWriteFileRepository>();
    }
}