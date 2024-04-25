using School.API.Core.DbContext;
using School.API.Core.Entities;
using School.API.Core.Interfaces;

namespace School.API.Core.Services
{
    public class InvoiceService: IInvoice
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public InvoiceService(ApplicationDbContext applicationDbContext) {
            _applicationDbContext = applicationDbContext;
        }

        public string createInvoice(InvoiceGenerate invoiceGenerate)
        {
            _applicationDbContext.InvoiceGenerates.Add(invoiceGenerate);
            _applicationDbContext.SaveChanges();
            return "Invoice created Succcessfully.";
        }

        public List<InvoiceGenerate> getInvoicesById(int id)
        {
            throw new NotImplementedException();
        }

        public int lastInvoiceId()
        {
            return _applicationDbContext.InvoiceGenerates.ToList().Count > 0 ? _applicationDbContext.InvoiceGenerates.Max(x => x.invoiceId) : 0;
        }
    }
}
