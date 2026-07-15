namespace Banking.Application.DTOs.Transaction;

public class TransactionDto
{
    public Guid Id { get; set; }

    public Guid BankAccountId { get; set; }

    public string Type { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }
}