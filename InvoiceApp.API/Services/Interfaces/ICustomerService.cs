using InvoiceApp.API.DTOs.CustomerDTOs;

namespace InvoiceApp.API.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAll();
        Task<bool> DeleteAsync(int id);

        Task<CustomerDTO> GetByIdAsync(int id);
        Task<CustomerDTO> CreateAsync(CreateCustomerDTO dto);
        Task<CustomerDTO> UpdateAsync(int id,  UpdateCustomerDTO dto);
    }
}
