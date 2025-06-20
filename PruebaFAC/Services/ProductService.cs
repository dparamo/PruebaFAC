using Microsoft.EntityFrameworkCore;
using PruebaFAC.Context;
using PruebaFAC.Dto;
using PruebaFAC.Entities;
using PruebaFAC.Interfaces;

namespace PruebaFAC.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();

            if (products == null || !products.Any())
            {
                return new List<ProductDto>
        {
            new ProductDto
            {
                Error = true,
                Message = "No hay productos registrados."
            }
        };
            }

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name ?? string.Empty,
                UnitPrice = p.UnitPrice,
                Stock = p.Stock,
                Error = false,
                Message = "Producto cargado correctamente."
            }).ToList();
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return new ProductDto
                {
                    Error = true,
                    Message = "Producto no encontrado."
                };

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name ?? string.Empty,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                Error = false,
                Message = "Producto encontrado correctamente."
            };
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                UnitPrice = dto.UnitPrice,
                Stock = dto.Stock
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                Error = false,
                Message = "Producto creado"
            };
        }

        public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return new ProductDto { Error = true, Message = "Producto no encontrado" };

            product.Name = dto.Name;
            product.UnitPrice = dto.UnitPrice;
            product.Stock = dto.Stock;

            await _context.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                UnitPrice = product.UnitPrice,
                Stock = product.Stock,
                Error = false,
                Message = "Producto actualizado"
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
