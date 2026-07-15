namespace Banking.Application.DTOs.Transaction;

/// <summary>
/// Response payload representing a financial transaction.
/// </summary>
public class TransactionDto
{
    /// <summary>The transaction's unique identifier.</summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid Id { get; set; }

    /// <summary>The unique identifier of the associated bank account.</summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid BankAccountId { get; set; }

    /// <summary>The type of transaction: Deposit or Withdrawal.</summary>
    /// <example>Deposit</example>
    public string Type { get; set; } = string.Empty;

    /// <summary>The transaction amount.</summary>
    /// <example>500.00</example>
    public decimal Amount { get; set; }

    /// <summary>The account balance after this transaction was applied.</summary>
    /// <example>1500.00</example>
    public decimal BalanceAfterTransaction { get; set; }

    /// <summary>The UTC date and time when the transaction was created.</summary>
    /// <example>2026-07-15T12:00:00</example>
    public DateTime CreatedAt { get; set; }
}