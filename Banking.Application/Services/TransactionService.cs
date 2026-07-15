using Banking.Application.DTOs.Transaction;
using Banking.Application.Interfaces;
using Banking.Application.Mappings;
using Banking.Domain.Entities;
using Banking.Domain.Enums;
using Banking.Domain.Exceptions;
using Banking.Domain.Interfaces;

namespace Banking.Application.Services;

public class TransactionService(
    IBankAccountRepository accountRepository,
    ITransactionRepository transactionRepository,
    IUnitOfWork unitOfWork)
    : ITransactionService
{
    public async Task<TransactionDto> DepositAsync(
        string accountNumber,
        string idempotencyKey,
        DepositDto dto)
    {
        var existingTransaction =
            await transactionRepository
                .GetByIdempotencyKeyAsync(idempotencyKey);

        if (existingTransaction != null)
        {
            return existingTransaction.ToResponseDto();
        }


        await unitOfWork.BeginTransactionAsync();

        try
        {
            var account = await GetAccountAsync(accountNumber);


            account.Deposit(dto.Amount);


            var transaction = CreateTransaction(
                account,
                TransactionType.Deposit,
                dto.Amount,
                idempotencyKey);


            var createdTransaction =
                await transactionRepository
                    .CreateAsync(transaction);


            await unitOfWork.CommitAsync();


            return createdTransaction.ToResponseDto();
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }


    public async Task<TransactionDto> WithdrawAsync(
        string accountNumber,
        string idempotencyKey,
        WithdrawDto dto)
    {
        var existingTransaction =
            await transactionRepository
                .GetByIdempotencyKeyAsync(idempotencyKey);

        if (existingTransaction != null)
        {
            return existingTransaction.ToResponseDto();
        }


        await unitOfWork.BeginTransactionAsync();

        try
        {
            var account = await GetAccountAsync(accountNumber);


            account.Withdraw(dto.Amount);


            var transaction = CreateTransaction(
                account,
                TransactionType.Withdrawal,
                dto.Amount,
                idempotencyKey);


            var createdTransaction =
                await transactionRepository
                    .CreateAsync(transaction);


            await unitOfWork.CommitAsync();


            return createdTransaction.ToResponseDto();
        }
        catch
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }


    private async Task<BankAccount> GetAccountAsync(
        string accountNumber)
    {
        return await accountRepository
            .GetByAccountNumberAsync(accountNumber)
            ?? throw new NotFoundException(
                "La cuenta no fue encontrada");
    }


    private Transaction CreateTransaction(
        BankAccount account,
        TransactionType type,
        decimal amount,
        string idempotencyKey)
    {
        return new Transaction(
            account.Id,
            type,
            amount,
            idempotencyKey);
    }
}