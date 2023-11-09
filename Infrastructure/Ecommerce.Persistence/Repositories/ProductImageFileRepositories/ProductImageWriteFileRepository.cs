using Application.Repositories.ProductImageFileRepositories;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.ProductImageFileRepositories;
public class ProductImageWriteFileRepository : WriteRepository<ProductImageFile>, IProductImageFileWriteRepository
{
    public ProductImageWriteFileRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}
