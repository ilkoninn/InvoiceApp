using InvoiceApp.API.DTOs.InvoiceDTOs;
using InvoiceApp.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _customerService;

        public InvoiceController(IInvoiceService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customerService.GetAll();

            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null || customer.Id == 0)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdInvoice = await _customerService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = createdInvoice.Id }, createdInvoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInvoiceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedInvoice = await _customerService.UpdateAsync(id, dto);

            if (updatedInvoice == null || updatedInvoice.Id == 0)
                return NotFound();

            return Ok(updatedInvoice);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _customerService.Delete(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
