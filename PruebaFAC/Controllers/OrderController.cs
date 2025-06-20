using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaFAC.Dto;
using PruebaFAC.Interfaces;

namespace PruebaFAC.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _orderService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _orderService.GetByIdAsync(id);
        if (result.Error)
            return NotFound(result);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Datos inválidos");

        var result = await _orderService.CreateAsync(dto);
        if (result.Error)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("{orderId}/items")]
    public async Task<IActionResult> UpdateItems(Guid orderId, [FromBody] List<CreateItemDto> itemsDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Datos inválidos");

        var result = await _orderService.UpdateItemsAsync(orderId, itemsDto);
        if (result.Error)
            return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _orderService.DeleteAsync(id);
        if (result.Error)
            return NotFound(result);
        return Ok(result);
    }

    [HttpGet("customer/{customerId}")]
    public async Task<IActionResult> GetByCustomer(Guid customerId)
    {
        var result = await _orderService.GetByCustomerIdAsync(customerId);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
    [FromQuery] string? status,
    [FromQuery] DateTime? date,
    [FromQuery] Guid? customerId) 
    {
        var result = await _orderService.SearchAsync(status, date, customerId);
        return Ok(result);
    }


}