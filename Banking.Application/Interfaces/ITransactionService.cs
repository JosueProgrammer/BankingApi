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

    Task<Banking.Application.DTOs.Common.PagedResultDto<TransactionDto>> GetHistoryAsync(
        string accountNumber,
        int page,
        int pageSize);
}