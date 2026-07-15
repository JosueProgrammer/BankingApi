namespace Banking.Application.DTOs.Account;

/// <summary>
/// Request payload for creating a new bank account.
/// </summary>
public class CreateBankAccountDto
{
    /// <summary>The unique identifier of the customer who will own the account.</summary>
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid CustomerId { get; set; }

    /// <summary>The initial deposit balance for the account. Must be zero or greater.</summary>
    /// <example>1000.00</example>
    public decimal InitialBalance { get; set; }
}