using InvoiceApp.API.Entities.Commons;

namespace InvoiceApp.API.Entities
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal Tax { get; set; }
        public decimal NetTotal { get; set; }

        // Foreign Keys
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public ICollection<InvoiceLine>? InvoiceLines { get; set; }
    }
}
