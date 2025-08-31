using InvoiceApp.API.DTOs.Commons;

namespace InvoiceApp.API.DTOs.InvoiceDTOs
{
    public class InvoiceDTO : BaseEntityDTO
    {
        public int InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; } 
        public decimal TotalAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal Tax { get; set; }
        public decimal NetTotal { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; } 
    }
}
