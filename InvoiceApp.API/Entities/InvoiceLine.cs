using InvoiceApp.API.Entities.Commons;

namespace InvoiceApp.API.Entities
{
    public class InvoiceLine : BaseEntity
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Foreign Keys
        public int? InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }
    }
}
