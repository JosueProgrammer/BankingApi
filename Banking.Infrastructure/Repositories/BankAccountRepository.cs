using Banking.Domain.Entities;
using Banking.Domain.Interfaces;
using Banking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Repositories;

public class BankAccountRepository(
    BankingDbContext context)
    : IBankAccountRepository
{
    public async Task<BankAccount> CreateAsync(
        BankAccount account)
    {
        await context.BankAccounts.AddAsync(account);

        await context.SaveChangesAsync();

        return account;
    }


    public async Task<bool> ExistsByAccountNumberAsync(
        string accountNumber)
    {
        return await context.BankAccounts
            .AnyAsync(x => x.AccountNumber == accountNumber);
    }


    public async Task<BankAccount?> GetByIdAsync(
        Guid id)
    {
        return await context.BankAccounts
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BankAccount?> GetByAccountNumberAsync(
    string accountNumber)
    {
        return await context.BankAccounts
            .FirstOrDefaultAsync(x => x.AccountNumber == accountNumber);
    }
}