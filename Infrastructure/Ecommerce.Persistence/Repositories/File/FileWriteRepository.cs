using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;

namespace Ecomerce.Application.Repositories;
public class FileWriteRepository : WriteRepository<Ecommerce.Domain.Entities.File>,IFileWriteRepository
{
    public FileWriteRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}

