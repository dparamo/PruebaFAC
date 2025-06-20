using Microsoft.EntityFrameworkCore;
using PruebaFAC.Context;
using PruebaFAC.Dto;
using PruebaFAC.Entities;
using PruebaFAC.Interfaces;

namespace PruebaFAC.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDto> GetByIdAsync(Guid id)
    {
        var result = new OrderDto();
        try
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return new OrderDto { Error = true, Message = "Orden no encontrada." };

            result = new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductName = i.Product?.Name ?? "Producto desconocido", 
                    Quantity = i.Quantity,
                    UnitPrice = i.Product?.UnitPrice ?? 0
                }).ToList(),
                Error = false,
                Message = "Orden encontrada correctamente."
            };
        }
        catch (Exception ex)
        {
            result = new OrderDto { Error = true, Message = ex.Message };
        }

        return result;
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
        var result = new List<OrderDto>();
        try
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .ToListAsync();

            if (!orders.Any())
            {
                result.Add(new OrderDto { Error = true, Message = "No hay órdenes registradas." });
                return result;
            }

            result = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductName = i.Product?.Name ?? "Producto desconocido", 
                    Quantity = i.Quantity,
                    UnitPrice = i.Product?.UnitPrice ?? 0 
                }).ToList(),
                Error = false,
                Message = "Orden cargada correctamente."
            }).ToList();
        }
        catch (Exception ex)
        {
            result.Add(new OrderDto { Error = true, Message = ex.Message });
        }

        return result;
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
    {
        var result = new OrderDto();
        try
        {
            var orderItems = new List<OrderItem>();

            foreach (var item in dto.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product == null)
                    return new OrderDto { Error = true, Message = $"Producto con ID {item.ProductId} no encontrado." };

                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity
                });
            }

            var order = new Order
            {
                CustomerId = dto.CustomerId,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow,
                Items = orderItems
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            result = new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                Items = orderItems.Select(i => new OrderItemDto
                {
                    ProductName = i.Product?.Name ?? "Producto desconocido", 
                    Quantity = i.Quantity,
                    UnitPrice = i.Product?.UnitPrice ?? 0 
                }).ToList(),
                Error = false,
                Message = "Orden creada correctamente."
            };
        }
        catch (Exception ex)
        {
            result = new OrderDto { Error = true, Message = ex.Message };
        }

        return result;
    }

    public async Task<OrderDto> UpdateItemsAsync(Guid orderId, List<CreateItemDto> itemsDto)
    {
        var result = new OrderDto();
        try
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return new OrderDto { Error = true, Message = "Orden no encontrada." };

            _context.OrderItems.RemoveRange(order.Items);

            var newItems = new List<OrderItem>();
            foreach (var dto in itemsDto)
            {
                var product = await _context.Products.FindAsync(dto.ProductId);
                if (product == null)
                    return new OrderDto { Error = true, Message = $"Producto con ID {dto.ProductId} no encontrado." };

                newItems.Add(new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = dto.Quantity
                });
            }

            _context.OrderItems.AddRange(newItems);
            await _context.SaveChangesAsync();

            result = await GetByIdAsync(order.Id);
        }
        catch (Exception ex)
        {
            result = new OrderDto { Error = true, Message = ex.Message };
        }

        return result;
    }

    public async Task<OrderDto> DeleteAsync(Guid id)
    {
        var result = new OrderDto();
        try
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return new OrderDto { Error = true, Message = "Orden no encontrada." };

            _context.OrderItems.RemoveRange(order.Items);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            result = new OrderDto { Id = id, Error = false, Message = "Orden eliminada correctamente." };
        }
        catch (Exception ex)
        {
            result = new OrderDto { Error = true, Message = ex.Message };
        }

        return result;
    }

    public async Task<List<OrderDto>> GetByCustomerIdAsync(Guid customerId)
    {
        var result = new List<OrderDto>();
        try
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();

            if (!orders.Any())
            {
                result.Add(new OrderDto { Error = true, Message = "El cliente no tiene órdenes registradas." });
                return result;
            }

            result = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductName = i.Product?.Name ?? "Producto desconocido", // Fix for CS8602  
                    Quantity = i.Quantity,
                    UnitPrice = i.Product?.UnitPrice ?? 0 // Fix for CS8602  
                }).ToList(),
                Error = false,
                Message = "Orden cargada correctamente."
            }).ToList();
        }
        catch (Exception ex)
        {
            result.Add(new OrderDto { Error = true, Message = ex.Message });
        }

        return result;
    }

    public async Task<List<OrderDto>> SearchAsync(string? status, DateTime? date, Guid? customerId)
    {
        var result = new List<OrderDto>();

        try
        {
            var query = _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(o => EF.Functions.Like(o.Status, $"%{status.Trim()}%"));

            if (date.HasValue)
            {
                var start = date.Value.Date;
                var end = start.AddDays(1);
                query = query.Where(o => o.CreatedAt >= start && o.CreatedAt < end);
            }

            if (customerId.HasValue && customerId.Value != Guid.Empty)
                query = query.Where(o => o.CustomerId == customerId.Value);

            var orders = await query.ToListAsync();

            if (!orders.Any())
            {
                result.Add(new OrderDto { Error = true, Message = "No se encontraron órdenes con los filtros proporcionados." });
                return result;
            }

            result = orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                Items = order.Items.Select(i => new OrderItemDto
                {
                    ProductName = i.Product?.Name ?? "Producto desconocido", // Fix for CS8602  
                    Quantity = i.Quantity,
                    UnitPrice = i.Product?.UnitPrice ?? 0 // Fix for CS8602  
                }).ToList(),
                Error = false,
                Message = "Orden cargada correctamente."
            }).ToList();
        }
        catch (Exception ex)
        {
            result.Add(new OrderDto { Error = true, Message = ex.Message });
        }

        return result;
    }
}