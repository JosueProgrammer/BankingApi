using Banking.Application.DTOs.Account;
using Banking.Domain.Entities;

namespace Banking.Application.Mappings;

public static class BankAccountMapping
{
    public static BankAccountResponseDto ToResponseDto(
        this BankAccount account)
    {
        return new BankAccountResponseDto
        {
            Id = account.Id,
            CustomerId = account.CustomerId,
            AccountNumber = account.AccountNumber,
            Balance = account.Balance,
            CreatedAt = account.CreatedAt
        };
    }
}