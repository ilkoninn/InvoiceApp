namespace InvoiceApp.API.DTOs.Commons
{
    public class BaseEntityDTO
    {
        public int Id { get; set; }
        public DateTime RecordDate { get; set; }
        public string CreatedByFullName { get; set; }
    }
}
