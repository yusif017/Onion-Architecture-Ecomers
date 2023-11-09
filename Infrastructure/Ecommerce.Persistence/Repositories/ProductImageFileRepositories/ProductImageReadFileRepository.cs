using Ecomerce.Application.Repositories.ProductImageFileRepositories;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.ProductImageFileRepositories;
public class ProductImageReadFileRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
{
    public ProductImageReadFileRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}
