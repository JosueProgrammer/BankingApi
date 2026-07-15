namespace Banking.Application.DTOs.Account;

public class CreateBankAccountDto
{
    /// <example>3fa85f64-5717-4562-b3fc-2c963f66afa6</example>
    public Guid CustomerId { get; set; }

    /// <example>1000.00</example>
    public decimal InitialBalance { get; set; }
}