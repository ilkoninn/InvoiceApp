using AutoMapper;
using InvoiceApp.API.DAL;
using InvoiceApp.API.DTOs.CustomerDTOs;
using InvoiceApp.API.Entities;
using InvoiceApp.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InvoiceApp.API.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDTO> CreateAsync(CreateCustomerDTO dto)
        {
            var customer = _mapper.Map<Customer>(dto);

            customer.TaxNumber = await GenerateTaxNumber();

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            
            var result = _context.Customers.Remove(entity);
            await _context.SaveChangesAsync();

            return result != null;
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            var customers = _context.Customers
                .Include(c => c.CreatedBy);

            return customers.Select(x => _mapper.Map<CustomerDTO>(x)); ;
        }

        public async Task<CustomerDTO> GetByIdAsync(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.CreatedBy)
                .FirstOrDefaultAsync(x => x.Id == id) ?? new();

            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<CustomerDTO> UpdateAsync(int id, UpdateCustomerDTO dto)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customer == null) return new CustomerDTO();

            var result = _mapper.Map(dto, customer);

            _context.Customers.Update(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerDTO>(result);
        }

        // Supporting methods
        private async Task<string> GenerateTaxNumber()
        {
            var lastCustomer = await _context.Customers
                .OrderByDescending(c => c.Id)
                .FirstOrDefaultAsync();

            int nextNumber = 1;

            if (lastCustomer != null && !string.IsNullOrEmpty(lastCustomer.TaxNumber))
            {
                var parts = lastCustomer.TaxNumber.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[1], out int lastNumber))
                {
                    nextNumber = lastNumber + 1;
                }
            }

            return $"C-{nextNumber:D4}";
        }
    }
}
