using InvoiceApp.API.DTOs.AuthDTOs;
using InvoiceApp.API.Entities.Indenties;

namespace InvoiceApp.API.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponseDTO> LoginAsync(LoginDTO dto);
        Task<User> CheckUserNotFoundAsync(string id);
        public Task RegisterAsync(RegisterDTO dto);
    }
}
