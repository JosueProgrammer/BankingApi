using Banking.Application.DTOs.Transaction;

namespace Banking.Application.Interfaces;

public interface ITransactionService
{
    Task<TransactionDto> DepositAsync(
        string accountNumber,
        string idempotencyKey,
        DepositDto dto);


    Task<TransactionDto> WithdrawAsync(
        string accountNumber,
        string idempotencyKey,
        WithdrawDto dto);
}