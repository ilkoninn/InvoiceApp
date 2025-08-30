using InvoiceApp.API.Entities.Indenties;

namespace InvoiceApp.API.Entities.Commons
{
    public class BaseEntity
    {
        // Primary Key
        public int Id { get; set; }

        // Common Properties
        public DateTime RecordDate { get; set; }

        public string CreatedById { get; set; } 
        public User CreatedBy { get; set; }
    }
}
