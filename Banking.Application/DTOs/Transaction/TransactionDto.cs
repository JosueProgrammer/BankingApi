namespace Banking.Application.DTOs.Transaction;

public class TransactionDto
{
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid Id { get; set; }

    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid BankAccountId { get; set; }

    /// <example>Deposit</example>
    public string Type { get; set; } = string.Empty;

    /// <example>500.00</example>
    public decimal Amount { get; set; }

    /// <example>1500.00</example>
    public decimal BalanceAfterTransaction { get; set; }

    /// <example>2026-07-15T12:00:00</example>
    public DateTime CreatedAt { get; set; }
}