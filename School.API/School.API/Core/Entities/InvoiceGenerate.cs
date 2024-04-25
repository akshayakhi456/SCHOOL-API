using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace School.API.Core.Entities
{
    public class InvoiceGenerate
    {
        [Key]
        public int id { get; set; }
        public int invoiceId { get; set; }
        public int studentId { get; set; }
        public byte[] invoicePhoto { get; set;}
    }
}
