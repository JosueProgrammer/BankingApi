using Banking.Application.DTOs.Transaction;
using Banking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController(
    ITransactionService transactionService)
    : ControllerBase
{
    [HttpPost("{accountNumber}/deposit")]
    public async Task<ActionResult<TransactionDto>> Deposit(
        string accountNumber,
        DepositDto dto,
        [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
    {
        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            return BadRequest(
                "El header Idempotency-Key es obligatorio.");
        }

        if (idempotencyKey.Length > 100)
        {
            return BadRequest(
                "El Idempotency-Key no puede superar 100 caracteres.");
        }

        var result = await transactionService.DepositAsync(
            accountNumber,
            idempotencyKey,
            dto);

        return Ok(result);
    }


    [HttpPost("{accountNumber}/withdraw")]
    public async Task<ActionResult<TransactionDto>> Withdraw(
        string accountNumber,
        WithdrawDto dto,
        [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
    {
        if (string.IsNullOrWhiteSpace(idempotencyKey))
        {
            return BadRequest(
                "El header Idempotency-Key es obligatorio.");
        }

        if (idempotencyKey.Length > 100)
        {
            return BadRequest(
                "El Idempotency-Key no puede superar 100 caracteres.");
        }

        var result = await transactionService.WithdrawAsync(
            accountNumber,
            idempotencyKey,
            dto);

        return Ok(result);
    }
}