

using Ecomerce.Application.Repositories.FileRepositories;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;

namespace Ecommerce.Persistence.Repositories.FileRepositories;
public class FileWriteRepositories : WriteRepository<Ecommerce.Domain.Entities.File>, IFileWriteRepositories
{
    public FileWriteRepositories(EcommerceAPIDbContext context) : base(context)
    {
    }
}
