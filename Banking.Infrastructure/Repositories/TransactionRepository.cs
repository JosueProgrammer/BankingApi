using Banking.Domain.Entities;
using Banking.Domain.Interfaces;
using Banking.Infrastructure.Persistence;

namespace Banking.Infrastructure.Repositories;

public class TransactionRepository(
    BankingDbContext context)
    : ITransactionRepository
{
    public async Task<Transaction> CreateAsync(
        Transaction transaction)
    {
        await context.Transactions.AddAsync(transaction);

        await context.SaveChangesAsync();

        return transaction;
    }
}