using Banking.Domain.Entities;
using Banking.Domain.Interfaces;
using Banking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Repositories;

public class CustomerRepository(
    BankingDbContext context)
    : ICustomerRepository
{
    public async Task<Customer> CreateAsync(
        Customer customer)
    {
        await context.Customers.AddAsync(customer);

        await context.SaveChangesAsync();

        return customer;
    }


    public async Task<Customer?> GetByIdAsync(
        Guid id)
    {
        return await context.Customers
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}