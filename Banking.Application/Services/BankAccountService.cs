using Banking.Application.DTOs.Account;
using Banking.Application.Interfaces;
using Banking.Application.Mappings;
using Banking.Domain.Entities;
using Banking.Domain.Exceptions;
using Banking.Domain.Interfaces;

namespace Banking.Application.Services;

public class BankAccountService(
    IBankAccountRepository accountRepository,
    ICustomerRepository customerRepository,
    IAccountNumberGenerator accountNumberGenerator)
    : IBankAccountService
{
    public async Task<BankAccountResponseDto> CreateAccountAsync(CreateBankAccountDto dto)
    {
        var customer = await customerRepository.GetByIdAsync(dto.CustomerId)
            ?? throw new NotFoundException("El cliente no fue encontrado");

        var accountNumber = await GenerateUniqueAccountNumberAsync();

        var account = new BankAccount(
            customer.Id,
            accountNumber,
            dto.InitialBalance);

        var createdAccount = await accountRepository.CreateAsync(account);

        return createdAccount.ToResponseDto();
    }


    private async Task<string> GenerateUniqueAccountNumberAsync()
    {
        const int maxAttempts = 5;

        for (int i = 0; i < maxAttempts; i++)
        {
            var accountNumber = accountNumberGenerator.Generate();

            if (!await accountRepository.ExistsByAccountNumberAsync(accountNumber))
            {
                return accountNumber;
            }
        }

        throw new BusinessException(
            "No fue posible generar un número de cuenta único.");
    }

    public async Task<BalanceDto> GetBalanceAsync(
    string accountNumber)
    {
        var account = await accountRepository
            .GetByAccountNumberAsync(accountNumber)
            ?? throw new NotFoundException(
                "La cuenta no fue encontrada");


        return new BalanceDto
        {
            AccountNumber = account.AccountNumber,
            Balance = account.Balance
        };
    }

}