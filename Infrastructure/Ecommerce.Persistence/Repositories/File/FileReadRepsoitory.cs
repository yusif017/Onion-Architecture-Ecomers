

using Ecomerce.Application.Repositories;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories;
public class FileReadRepsoitory : ReadRepository<Ecommerce.Domain.Entities.File>, IFileReadRepsoitory
{
    public FileReadRepsoitory(EcommerceAPIDbContext context) : base(context)
    {
    }
}

