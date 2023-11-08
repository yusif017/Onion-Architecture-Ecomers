using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;

namespace Ecomerce.Application.Repositories;
public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
{
    public InvoiceFileWriteRepository(EcommerceAPIDbContext context) : base(context)
    {
    }
}
