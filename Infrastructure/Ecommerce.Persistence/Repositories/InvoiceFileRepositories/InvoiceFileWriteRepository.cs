using Ecomerce.Application.Repositories.InvoiceFileRepositories;
using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;

namespace Ecommerce.Persistence.Repositories.InvoiceFileRepositories;
public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}
