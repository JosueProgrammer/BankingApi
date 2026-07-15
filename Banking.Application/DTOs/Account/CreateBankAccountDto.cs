namespace Banking.Application.DTOs.Account;

public class CreateBankAccountDto
{
    public Guid CustomerId { get; set; }

    public decimal InitialBalance { get; set; }
}