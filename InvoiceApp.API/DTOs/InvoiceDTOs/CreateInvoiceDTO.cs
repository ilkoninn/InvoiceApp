namespace InvoiceApp.API.DTOs.InvoiceDTOs
{
    public class CreateInvoiceDTO
    {
        public DateTime InvoiceDate { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal Tax { get; set; }

        public int CustomerId { get; set; }

        public List<CreateInvoiceLineDTO> InvoiceLines { get; set; }
    }

    public class CreateInvoiceLineDTO
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
