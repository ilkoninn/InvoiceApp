namespace InvoiceApp.API.DTOs.InvoiceDTOs
{
    public class UpdateInvoiceDTO
    {
        public DateTime InvoiceDate { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal Tax { get; set; }

        public int CustomerId { get; set; }

        public List<UpdateInvoiceLineDTO> InvoiceLines { get; set; }
    }

    public class UpdateInvoiceLineDTO
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
