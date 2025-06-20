using PruebaFAC.Dto;

namespace PruebaFAC.Interfaces;

public interface IOrderService
{
    Task<List<OrderDto>> GetAllAsync();
    Task<OrderDto> GetByIdAsync(Guid id);
    Task<OrderDto> CreateAsync(CreateOrderDto dto);
    Task<OrderDto> UpdateItemsAsync(Guid orderId, List<CreateItemDto> itemsDto);
    Task<OrderDto> DeleteAsync(Guid id);

    // Métodos adicionales requeridos por los endpoints
    Task<List<OrderDto>> GetByCustomerIdAsync(Guid customerId);
    Task<List<OrderDto>> SearchAsync(string? status, DateTime? date, Guid? customerId);
}
