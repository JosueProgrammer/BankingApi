using Banking.Domain.Exceptions;

namespace Banking.Domain.Entities;

public class BankAccount
{
    public Guid Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public string AccountNumber { get; private set; } = null!;

    public decimal Balance { get; private set; }

    public DateTime CreatedAt { get; private set; }


    private BankAccount()
    {
    }


    public BankAccount(
        Guid customerId,
        string accountNumber,
        decimal initialBalance)
    {
        if (initialBalance < 0)
        {
            throw new BusinessException(
                "El saldo inicial no puede ser negativo");
        }


        Id = Guid.NewGuid();

        CustomerId = customerId;

        AccountNumber = accountNumber;

        Balance = initialBalance;

        CreatedAt = DateTime.UtcNow;
    }
}