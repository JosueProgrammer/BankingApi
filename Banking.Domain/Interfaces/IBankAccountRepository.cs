using Banking.Domain.Entities;

namespace Banking.Domain.Interfaces;

public interface IBankAccountRepository
{
    Task<BankAccount> CreateAsync(
        BankAccount account);


    Task<bool> ExistsByAccountNumberAsync(
        string accountNumber);


    Task<BankAccount?> GetByIdAsync(
        Guid id);
}