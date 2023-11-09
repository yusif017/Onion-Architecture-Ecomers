
using Ecomerce.Application.Repositories.InvoiceFileRepositories;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.InvoiceFileRepositories;
public class InvoiceFileReadRepository : ReadRepository<InvoiceFile>, IInvoiceFileReadRepository
{
    public InvoiceFileReadRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}

