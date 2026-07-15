namespace Banking.Application.DTOs.Account;
public class BankAccountResponseDto
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }
}