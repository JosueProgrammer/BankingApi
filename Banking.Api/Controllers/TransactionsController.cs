using Banking.Application.DTOs.Transaction;
using Banking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers;

/// <summary>
/// Handles financial transactions: deposits, withdrawals, and transaction history.
/// </summary>
[ApiController]
[Route("api/transactions")]
public class TransactionsController(
    ITransactionService transactionService)
    : ControllerBase
{
    /// <summary>
    /// Performs a deposit into a bank account.
    /// </summary>
    /// <param name="accountNumber">The destination account number.</param>
    /// <param name="dto">Deposit data.</param>
    /// <param name="idempotencyKey">Idempotency key to prevent duplicate transactions. <example>deposit-001</example></param>
    [HttpPost("{accountNumber}/deposit")]
    public async Task<ActionResult<TransactionDto>> Deposit(
        string accountNumber,
        DepositDto dto,
        [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
    {
        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            return BadRequest(
                "The Idempotency-Key header is required.");
        }

        if (idempotencyKey.Length > 100)
        {
            return BadRequest(
                "The Idempotency-Key cannot exceed 100 characters.");
        }

        var result = await transactionService.DepositAsync(
            accountNumber,
            idempotencyKey,
            dto);

        return Ok(result);
    }


    /// <summary>
    /// Performs a withdrawal from a bank account.
    /// </summary>
    /// <param name="accountNumber">The source account number.</param>
    /// <param name="dto">Withdrawal data.</param>
    /// <param name="idempotencyKey">Idempotency key to prevent duplicate transactions. <example>withdraw-001</example></param>
    [HttpPost("{accountNumber}/withdraw")]
    public async Task<ActionResult<TransactionDto>> Withdraw(
        string accountNumber,
        WithdrawDto dto,
        [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
    {
        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            return BadRequest(
                "The Idempotency-Key header is required.");
        }

        if (idempotencyKey.Length > 100)
        {
            return BadRequest(
                "The Idempotency-Key cannot exceed 100 characters.");
        }

        var result = await transactionService.WithdrawAsync(
            accountNumber,
            idempotencyKey,
            dto);

        return Ok(result);
    }


    /// <summary>
    /// Retrieves the transaction history for a bank account with pagination.
    /// </summary>
    /// <param name="accountNumber">The account number.</param>
    /// <param name="page">Current page number (default: 1).</param>
    /// <param name="pageSize">Number of records per page (maximum: 100).</param>
    /// <param name="type">Optional filter by transaction type (Deposit or Withdrawal).</param>
    /// <returns>Paginated list of transactions.</returns>
    [HttpGet("{accountNumber}/history")]
    public async Task<ActionResult<Banking.Application.DTOs.Common.PagedResultDto<TransactionDto>>> GetHistory(
        [FromRoute] string accountNumber,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? type = null)
    {
        var result = await transactionService.GetHistoryAsync(accountNumber, page, pageSize, type);
        return Ok(result);
    }
}