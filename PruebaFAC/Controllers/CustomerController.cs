using Microsoft.AspNetCore.Mvc;
using PruebaFAC.Dto;
using PruebaFAC.Interfaces;

namespace PruebaFAC.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

    // GET: api/customer
    [HttpGet]

    public async Task<ActionResult<List<CustomerDto>>> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    // GET: api/customer/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDto>> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        if (result.Error)
            return NotFound(result);
        return Ok(result);
    }

    // POST: api/customer
    [HttpPost]
    public async Task<ActionResult<CustomerDto>> Create([FromBody] CreateCustomerDto dto)
    {
        var result = await _service.CreateAsync(dto);
        if (result.Error)
            return BadRequest(result);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerDto>> Update(Guid id, [FromBody] UpdateCustomerDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        if (result.Error)
            return NotFound(result);
        return Ok(result);
    }

    // DELETE: api/customer/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomerDto>> Delete(Guid id)
    {
        var result = await _service.DeleteAsync(id);
        if (result.Error)
            return NotFound(result);
        return Ok(result);
    }
}
