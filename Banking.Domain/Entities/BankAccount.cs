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
                "Initial balance cannot be negative.");
        }


        Id = Guid.NewGuid();

        CustomerId = customerId;

        AccountNumber = accountNumber;

        Balance = initialBalance;

        CreatedAt = DateTime.UtcNow;
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new BusinessException(
                "Deposit amount must be greater than zero.");
        }

        Balance += amount;
    }


    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new BusinessException(
                "Withdrawal amount must be greater than zero.");
        }


        if (Balance < amount)
        {
            throw new InsufficientFundsException(
                "Insufficient funds to complete the withdrawal.");
        }


        Balance -= amount;
    }
}