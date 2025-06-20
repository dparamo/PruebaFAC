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
        OrderDto result = new();
        try
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                result.Error = true;
                result.Message = "Orden no encontrada.";
                return result;
            }

            result.Id = order.Id;
            result.CustomerId = order.CustomerId;
            result.Status = order.Status;
            result.CreatedAt = order.CreatedAt;
            result.Items = order.Items.Select(i => new OrderItemDto
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();

            result.Error = false;
            result.Message = "Orden encontrada correctamente.";
        }
        catch (Exception ex)
        {
            result.Error = true;
            result.Message = ex.Message;
        }

        return result;
    }

    
    
    public async Task<List<OrderDto>> GetAllAsync()
    {
        List<OrderDto> result = new();
        try
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ToListAsync();

            if (orders == null || !orders.Any())
            {
                result.Add(new OrderDto
                {
                    Error = true,
                    Message = "No hay órdenes registradas."
                });
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
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                Error = false,
                Message = "Orden cargada correctamente."
            }).ToList();
        }
        catch (Exception ex)
        {
            result = new List<OrderDto>
        {
            new OrderDto
            {
                Error = true,
                Message = ex.Message
            }
        };
        }

        return result;
    }

    public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
    {
        OrderDto result = new();
        try
        {
            var order = new Order
            {
                CustomerId = dto.CustomerId,
                CreatedAt = DateTime.UtcNow,
                Status = dto.Status,
                Items = dto.Items.Select(i => new OrderItem
                {
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            result.Id = order.Id;
            result.CustomerId = order.CustomerId;
            result.Status = order.Status;
            result.CreatedAt = order.CreatedAt;
            result.Items = dto.Items.Select(i => new OrderItemDto
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList(); 
            result.Error = false;
            result.Message = "Orden creada correctamente.";
        }
        catch (Exception ex)
        {
            result.Error = true;
            result.Message = ex.Message;
        }
        return result;
    }


    public async Task<OrderDto> UpdateItemsAsync(Guid orderId, List<CreateItemDto> itemsDto)
    {
        OrderDto result = new();
        try
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                result.Error = true;
                result.Message = "Orden no encontrada.";
                return result;
            }

            // Borrar items antiguos
            _context.OrderItems.RemoveRange(order.Items);

            // Agregar nuevos items
            var newItems = itemsDto.Select(i => new OrderItem
            {
                OrderId = order.Id,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();

            _context.OrderItems.AddRange(newItems);

            await _context.SaveChangesAsync();

            result.Id = order.Id;
            result.CustomerId = order.CustomerId;
            result.Status = order.Status;
            result.CreatedAt = order.CreatedAt;
            result.Items = newItems.Select(i => new OrderItemDto
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();
            result.Error = false;
            result.Message = "Items actualizados correctamente.";
        }
        catch (Exception ex)
        {
            result.Error = true;
            result.Message = ex.Message;
        }
        return result;
    }


    public async Task<OrderDto> DeleteAsync(Guid id)
    {
        OrderDto result = new();
        try
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                result.Error = true;
                result.Message = "Orden no encontrada.";
                return result;
            }

            _context.OrderItems.RemoveRange(order.Items);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            result.Id = order.Id;
            result.CustomerId = order.CustomerId;
            result.Status = order.Status;
            result.CreatedAt = order.CreatedAt;
            result.Items = order.Items.Select(i => new OrderItemDto
            {
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList();
            result.Error = false;
            result.Message = "Orden eliminada correctamente.";
        }
        catch (Exception ex)
        {
            result.Error = true;
            result.Message = ex.Message;
        }
        return result;
    }

    public async Task<List<OrderDto>> GetByCustomerIdAsync(Guid customerId)
    {
        List<OrderDto> result = new();
        try
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();

            if (!orders.Any())
            {
                result.Add(new OrderDto
                {
                    Error = true,
                    Message = "El cliente no tiene órdenes registradas."
                });
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
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                Error = false,
                Message = "Orden cargada correctamente."
            }).ToList();
        }
        catch (Exception ex)
        {
            result = new List<OrderDto>
        {
            new OrderDto
            {
                Error = true,
                Message = ex.Message
            }
        };
        }

        return result;
    }


    public async Task<List<OrderDto>> SearchAsync(string? status, DateTime? date, Guid? customerId)
    {
        List<OrderDto> result = new();

        try
        {
            var query = _context.Orders
                .Include(o => o.Items)
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
                result.Add(new OrderDto
                {
                    Error = true,
                    Message = "No se encontraron órdenes con los filtros proporcionados."
                });
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
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList(),
                Error = false,
                Message = "Orden cargada correctamente."
            }).ToList();
        }
        catch (Exception ex)
        {
            result.Add(new OrderDto
            {
                Error = true,
                Message = ex.Message
            });
        }

        return result;
    }


}
