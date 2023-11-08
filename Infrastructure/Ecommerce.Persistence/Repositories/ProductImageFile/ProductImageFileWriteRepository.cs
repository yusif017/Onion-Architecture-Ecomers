using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;

namespace Ecomerce.Application.Repositories;
public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
{
    public ProductImageFileWriteRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}

