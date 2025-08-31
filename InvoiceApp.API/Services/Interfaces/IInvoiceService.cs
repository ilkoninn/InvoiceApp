using InvoiceApp.API.DTOs.InvoiceDTOs;

namespace InvoiceApp.API.Services.Interfaces
{
    public interface IInvoiceService
    {
        IEnumerable<InvoiceDTO> GetAll();
        bool Delete(int id);

        Task<InvoiceDTO> GetByIdAsync(int id);
        Task<InvoiceDTO> CreateAsync(CreateInvoiceDTO dto);
        Task<InvoiceDTO> UpdateAsync(int id, UpdateInvoiceDTO dto);
    }
}
