namespace Banking.Domain.Entities;

using Banking.Domain.Enums;

public class Transaction
{
    public Guid Id { get; private set; }

    public Guid BankAccountId { get; private set; }

    public string IdempotencyKey { get; private set; }

    public TransactionType Type { get; private set; }

    public decimal Amount { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public decimal BalanceAfterTransaction { get; private set; }


    private Transaction()
    {
    }


    public Transaction(
      Guid bankAccountId,
      TransactionType type,
      decimal amount,
      string idempotencyKey,
      decimal balanceAfterTransaction)
    {
        Id = Guid.NewGuid();
        BankAccountId = bankAccountId;
        Type = type;
        Amount = amount;
        IdempotencyKey = idempotencyKey;
        BalanceAfterTransaction = balanceAfterTransaction;
        CreatedAt = DateTime.UtcNow;
    }
}