using Ecomerce.Application.Repositories.FileRepositories;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;

namespace Ecommerce.Persistence.Repositories.FileRepositories;
public class FileReadRepositories : ReadRepository<Ecommerce.Domain.Entities.File>, IFileReadRepositories
{
    public FileReadRepositories(EcommerceAPIDbContext context) : base(context)
    {
    }
}
