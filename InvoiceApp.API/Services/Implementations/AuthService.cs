using App.Business.Helpers;
using AutoMapper;
using InvoiceApp.API.DTOs.AuthDTOs;
using InvoiceApp.API.Entities.Indenties;
using InvoiceApp.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InvoiceApp.API.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(
            UserManager<User> userManager,
            IConfiguration configuration,
            IMapper mapper)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<LoginResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.UsernameOrEmail);

            if (user == null)
                user = await _userManager.FindByNameAsync(dto.UsernameOrEmail);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new UnauthorizedAccessException("Email or password is incorrect.");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "RoleError";

            var token = JwtGenerator.GenerateToken(user, role, _configuration);

            return new LoginResponseDTO
            {
                Token = token
            };
        }

        public async Task RegisterAsync(RegisterDTO dto)
        {
            var userByEmail = await _userManager.FindByEmailAsync(dto.Email);

            if (userByEmail is not null)
                throw new Exception("User with this email already exists.");

            var userByUsername = await _userManager.FindByNameAsync(dto.UserName);

            if (userByUsername is not null)
                throw new Exception("User with this username already exists.");

            var user = _mapper.Map<User>(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"Failed to create user. Errors: {errors}");
            }

            await _userManager.AddToRoleAsync(user, "Moderator");
        }

        public async Task<User> CheckUserNotFoundAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                throw new Exception($"Entity of type {typeof(User).Name.ToLower()} not found.");

            return user;
        }
    }
}
