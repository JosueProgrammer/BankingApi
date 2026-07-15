namespace Banking.Application.DTOs.Transaction;

/// <summary>
/// Request payload for a withdrawal operation.
/// </summary>
public class WithdrawDto
{
    /// <summary>The amount to withdraw. Must be greater than zero.</summary>
    /// <example>200.00</example>
    public decimal Amount { get; set; }
}