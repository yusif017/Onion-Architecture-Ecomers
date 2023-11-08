using Ecommerce.Domain.Entities;
using Ecommerce.Persistence.Contexts;
using Ecommerce.Persistence.Repositories;

namespace Ecomerce.Application.Repositories;
public class InvoiceFileReadRepsoitory : ReadRepository<InvoiceFile> , IInvoiceFileReadRepsoitory
{
    public InvoiceFileReadRepsoitory(EcommerceAPIDbContext context) : base(context)
    {
    }
}
