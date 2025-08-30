using InvoiceApp.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace InvoiceApp.API.Entities.Indenties
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime RecordDate { get; set; } = DateTime.UtcNow;

        // Foriegn Keys 
        public ICollection<Invoice>? Invoices { get; set; }
        public ICollection<Customer>? Customers { get; set; }
        public ICollection<InvoiceLine>? InvoiceLines { get; set; } 
    }
}
