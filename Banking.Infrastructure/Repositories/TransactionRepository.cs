using Banking.Domain.Entities;
using Banking.Domain.Interfaces;
using Banking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Banking.Domain.Exceptions;

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

    public async Task<(IEnumerable<Transaction> Items, int TotalCount)> GetHistoryAsync(
        string accountNumber,
        int page,
        int pageSize,
        Banking.Domain.Enums.TransactionType? type = null)
    {
        var accountId = await context.Set<BankAccount>()
            .Where(a => a.AccountNumber == accountNumber)
            .Select(a => a.Id)
            .FirstOrDefaultAsync();

        if (accountId == Guid.Empty)
        {
           throw new NotFoundException(
            "La cuenta no fue encontrada.");
        }

        var query = context.Transactions
            .Where(t => t.BankAccountId == accountId);

        if (type.HasValue)
        {
            query = query.Where(t => t.Type == type.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }
}