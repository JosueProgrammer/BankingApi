namespace Banking.Domain.Entities;

public class BankAccount
{
    public Guid Id { get; set; }

    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;
}