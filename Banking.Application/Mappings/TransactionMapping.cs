using Banking.Application.DTOs.Transaction;
using Banking.Domain.Entities;

namespace Banking.Application.Mappings;

public static class TransactionMapping
{
    public static TransactionDto ToResponseDto(
        this Transaction transaction)
    {
        return new TransactionDto
        {
            Id = transaction.Id,
            BankAccountId = transaction.BankAccountId,
            Type = transaction.Type.ToString(),
            Amount = transaction.Amount,
            BalanceAfterTransaction = transaction.BalanceAfterTransaction,
            CreatedAt = transaction.CreatedAt
        };
    }
}