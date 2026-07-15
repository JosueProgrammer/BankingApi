using Banking.Domain.Entities;

namespace Banking.Domain.Interfaces;

public interface ICustomerRepository
{
    Task<Customer> CreateAsync(Customer customer);

    Task<Customer?> GetByIdAsync(Guid id);
}