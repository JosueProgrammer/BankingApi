using Banking.Application.DTOs.Account;

namespace Banking.Application.Interfaces;

public interface IBankAccountService
{
    Task<BankAccountResponseDto> CreateAccountAsync(
        CreateBankAccountDto dto);
        
    Task<BalanceDto> GetBalanceAsync(
    string accountNumber);
}