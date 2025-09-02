using InvoiceApp.API.DTOs.Commons;

namespace InvoiceApp.API.DTOs.InvoiceDTOs
{
    public class InvoiceDTO : BaseEntityDTO
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; } 
        public decimal TotalAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal Tax { get; set; }
        public decimal NetTotal { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public ICollection<InvoiceLineDTO> InvoiceLines { get; set; }
    }

    public class InvoiceLineDTO : BaseEntityDTO 
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
