using Banking.Domain.Entities;

namespace Banking.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> CreateAsync(
        Transaction transaction);

    Task<Transaction?> GetByIdempotencyKeyAsync(
    string idempotencyKey);
}