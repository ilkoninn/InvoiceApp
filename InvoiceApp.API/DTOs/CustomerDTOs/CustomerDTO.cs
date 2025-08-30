using InvoiceApp.API.DTOs.Commons;

namespace InvoiceApp.API.DTOs.CustomerDTOs
{
    public class CustomerDTO : BaseEntityDTO
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string TaxNumber { get; set; }
    }
}
