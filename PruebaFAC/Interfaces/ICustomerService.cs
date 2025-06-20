using PruebaFAC.Dto;

namespace PruebaFAC.Interfaces;

public interface ICustomerService
{
    Task<List<CustomerDto>> GetAllAsync();
    Task<CustomerDto> GetByIdAsync(Guid id);
    Task<CustomerDto> CreateAsync(CreateCustomerDto dto);  
    Task<CustomerDto> UpdateAsync(Guid id, UpdateCustomerDto dto); 
    Task<CustomerDto> DeleteAsync(Guid id);
}
