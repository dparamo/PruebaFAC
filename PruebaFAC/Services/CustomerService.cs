using Microsoft.EntityFrameworkCore;
using PruebaFAC.Context;
using PruebaFAC.Dto;
using PruebaFAC.Entities;
using PruebaFAC.Interfaces;

namespace PruebaFAC.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CustomerDto>> GetAllAsync()
    {
        List<CustomerDto> result = new();
        try
        {
            var customers = await _context.Customers.ToListAsync();

            if (customers == null || !customers.Any())
            {
                result.Add(new CustomerDto
                {
                    Error = true,
                    Message = "No hay clientes registrados."
                });
                return result;
            }

            result = customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                Name = c.Name,
                Username = c.Username,
                Email = c.Email,
                Error = false,
                Message = "Cliente cargado correctamente."
            }).ToList();
        }
        catch (Exception ex)
        {
            result = new List<CustomerDto>
        {
            new CustomerDto
            {
                Error = true,
                Message = ex.Message
            }
        };
        }
        return result;
    }

    public async Task<CustomerDto> GetByIdAsync(Guid id)
    {
        CustomerDto result = new();
        try
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                result.Error = true;
                result.Message = "Cliente no encontrado.";
                return result;
            }

            result.Id = customer.Id;
            result.Name = customer.Name;
            result.Username = customer.Username;
            result.Email = customer.Email;
            result.Error = false;
            result.Message = "Cliente encontrado correctamente.";
        }
        catch (Exception ex)
        {
            result.Error = true;
            result.Message = ex.Message;
        }
        return result;
    }

    public async Task<CustomerDto> CreateAsync(CreateCustomerDto dto)
    {
        CustomerDto result = new();
        try
        {
            var customer = new Customer
            {
                Name = dto.Name,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            result.Id = customer.Id;
            result.Name = customer.Name;
            result.Username = customer.Username;
            result.Email = customer.Email;
            result.Error = false;
            result.Message = "Cliente creado correctamente.";
        }
        catch (Exception ex)
        {
            result.Error = true;
            result.Message = ex.Message;
        }
        return result;
    }


    public async Task<CustomerDto> UpdateAsync(Guid id, UpdateCustomerDto dto)
    {
        CustomerDto result = new();
        try
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return new CustomerDto
                {
                    Error = true,
                    Message = "Cliente no encontrado."
                };
            }

            customer.Name = dto.Name;
            customer.Email = dto.Email;
            customer.Username = dto.Username;

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            await _context.SaveChangesAsync();

            return new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Username = customer.Username,
                Email = customer.Email,
                Error = false,
                Message = "Cliente actualizado correctamente."
            };
        }
        catch (Exception ex)
        {
            return new CustomerDto
            {
                Error = true,
                Message = ex.Message
            };
        }
    }



    public async Task<CustomerDto> DeleteAsync(Guid id)
    {
        CustomerDto result = new();
        try
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                result.Error = true;
                result.Message = "Cliente no encontrado.";
                return result;
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            result.Id = customer.Id;
            result.Name = customer.Name;
            result.Error = false;
            result.Message = "Cliente eliminado correctamente.";
        }
        catch (Exception ex)
        {
            result.Error = true;
            result.Message = ex.Message;
        }
        return result;
    }
}
