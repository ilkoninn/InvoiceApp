using InvoiceApp.API.Services.Interfaces;
using System.Security.Claims;

namespace InvoiceApp.API.Services.Implementations
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return GetClaim(ClaimTypes.Name);
        }

        // Supporting methods
        private string GetClaim(string key)
        {
            var result = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "id")?.Value
                ?? _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault()?.Value;

            return result;
        }
    }
}
