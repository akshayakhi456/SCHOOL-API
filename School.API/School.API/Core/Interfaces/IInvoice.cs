using School.API.Core.Entities;

namespace School.API.Core.Interfaces
{
    public interface IInvoice
    {
        string createInvoice(InvoiceGenerate invoiceGenerate);

        int lastInvoiceId();

        List<InvoiceGenerate> getInvoicesById(int id);
    }
}
