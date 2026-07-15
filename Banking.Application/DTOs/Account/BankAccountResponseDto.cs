namespace Banking.Application.DTOs.Account;

/// <summary>
/// Response payload representing a bank account.
/// </summary>
public class BankAccountResponseDto
{
    /// <summary>The bank account's unique identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>The unique identifier of the account owner.</summary>
    public Guid CustomerId { get; set; }

    /// <summary>The account number assigned to this account.</summary>
    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>The current balance of the account.</summary>
    public decimal Balance { get; set; }

    /// <summary>The UTC date and time when the account was created.</summary>
    public DateTime CreatedAt { get; set; }
}