using Banking.Application.DTOs.Customer;

namespace Banking.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);

    Task<CustomerResponseDto?> GetCustomerByIdAsync(Guid id);
}