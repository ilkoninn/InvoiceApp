using AutoMapper;
using InvoiceApp.API.DAL;
using InvoiceApp.API.DTOs.InvoiceDTOs;
using InvoiceApp.API.Entities;
using InvoiceApp.API.MapperProfiles;
using InvoiceApp.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InvoiceApp.API.Services.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InvoiceService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InvoiceDTO> CreateAsync(CreateInvoiceDTO dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);

            invoice.TotalAmount = invoice.InvoiceLines.Sum(x => x.Quantity * x.Price);
            invoice.NetTotal = invoice.TotalAmount + (invoice.Tax * invoice.TotalAmount);

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            invoice.InvoiceNumber = $"Inv-{invoice.Id:D6}";
            await _context.SaveChangesAsync(); 

            return _mapper.Map<InvoiceDTO>(invoice);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Invoices
                .Include(x => x.InvoiceLines)
                .FirstOrDefaultAsync(x => x.Id == id);

            var result = _context.Invoices.Remove(entity);
            await _context.SaveChangesAsync();

            return result != null;
        }

        public IEnumerable<InvoiceDTO> GetAll()
        {
            var invoices = _context.Invoices?
                .Include(c => c.InvoiceLines)?
                .ThenInclude(c => c.CreatedBy)
                .Include(c => c.Customer)
                .Include(u => u.CreatedBy);

            return invoices.Select(x => _mapper.Map<InvoiceDTO>(x)); ;
        }

        public async Task<InvoiceDTO> GetByIdAsync(int id)
        {
            var invoice = await _context.Invoices
                .Include(c => c.InvoiceLines)?
                .ThenInclude(c => c.CreatedBy)
                .Include(c => c.Customer)
                .Include(u => u.CreatedBy)
                .FirstOrDefaultAsync(x => x.Id == id) ?? new();

            return _mapper.Map<InvoiceDTO>(invoice);
        }

        public async Task<InvoiceDTO> UpdateAsync(int id, UpdateInvoiceDTO dto)
        {
            var invoice = await _context.Invoices
                .Include(x => x.InvoiceLines)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (invoice == null) return new InvoiceDTO();

            if (invoice.InvoiceLines != null)
            {
                _context.InvoiceLines.RemoveRange(invoice.InvoiceLines);
            }

            var result = _mapper.Map(dto, invoice);

            result.TotalAmount = result.InvoiceLines.Sum(x => x.Quantity * x.Price);
            result.NetTotal = result.TotalAmount + (result.Tax * result.TotalAmount);

            _context.Invoices.Update(result);
            await _context.SaveChangesAsync();

            return _mapper.Map<InvoiceDTO>(result);
        }
    }
}
