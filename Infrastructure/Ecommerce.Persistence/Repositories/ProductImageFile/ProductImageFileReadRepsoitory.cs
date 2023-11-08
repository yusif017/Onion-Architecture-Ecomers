using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;

namespace Ecomerce.Application.Repositories;
public class ProductImageFileReadRepsoitory : ReadRepository<ProductImageFile>, IProductImageFileReadRepsoitory
{
    public ProductImageFileReadRepsoitory(EcommerceAPIDbContext context) : base(context)
    {
    }
}
