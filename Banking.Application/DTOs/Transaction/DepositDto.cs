namespace Banking.Application.DTOs.Transaction;

/// <summary>
/// Request payload for a deposit operation.
/// </summary>
public class DepositDto
{
    /// <summary>The amount to deposit. Must be greater than zero.</summary>
    /// <example>500.00</example>
    public decimal Amount { get; set; }
}