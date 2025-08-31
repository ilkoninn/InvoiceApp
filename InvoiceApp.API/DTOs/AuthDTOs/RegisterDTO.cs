using System.ComponentModel.DataAnnotations;

namespace InvoiceApp.API.DTOs.AuthDTOs
{
    public class RegisterDTO
    {
        public string FullName { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } 
    }
}
