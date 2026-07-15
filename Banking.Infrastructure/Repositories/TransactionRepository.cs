using Banking.Domain.Entities;
using Banking.Domain.Interfaces;
using Banking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Transaction?> GetByIdempotencyKeyAsync(
    string idempotencyKey)
    {
        return await context.Transactions
            .FirstOrDefaultAsync(x =>
                x.IdempotencyKey == idempotencyKey);
    }
}