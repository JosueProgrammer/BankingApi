namespace Banking.Application.DTOs.Account;

public class BalanceDto
{
    public string AccountNumber { get; set; } = string.Empty;

    public decimal Balance { get; set; }
}