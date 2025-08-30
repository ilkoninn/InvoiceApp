using InvoiceApp.API.Entities.Commons;

namespace InvoiceApp.API.Entities
{
    public class Invoice : BaseEntity
    {
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }

        // Foreign Keys
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }
    }
}
