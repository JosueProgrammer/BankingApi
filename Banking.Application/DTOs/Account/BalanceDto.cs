namespace Banking.Application.DTOs.Account;

/// <summary>
/// Response payload representing the current balance of a bank account.
/// </summary>
public class BalanceDto
{
    /// <summary>The account number.</summary>
    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>The current balance of the account.</summary>
    public decimal Balance { get; set; }
}