using InvoiceApp.API.Entities.Commons;

namespace InvoiceApp.API.Entities
{
    public class Customer : BaseEntity
    {
        public string Title { get; set; } 
        public string Address { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }

        // Foreign Keys
        public ICollection<Invoice>? Invoices { get; set; }
    }
}
