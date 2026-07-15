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
    /// <summary>
    /// Realiza un depósito en una cuenta bancaria.
    /// </summary>
    /// <param name="accountNumber">Número de cuenta destino.</param>
    /// <param name="dto">Datos del depósito.</param>
    /// <param name="idempotencyKey">Clave de idempotencia para evitar transacciones duplicadas. <example>deposit-001</example></param>
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


    /// <summary>
    /// Realiza un retiro de una cuenta bancaria.
    /// </summary>
    /// <param name="accountNumber">Número de cuenta origen.</param>
    /// <param name="dto">Datos del retiro.</param>
    /// <param name="idempotencyKey">Clave de idempotencia para evitar transacciones duplicadas. <example>withdraw-001</example></param>
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


    /// <summary>
    /// Obtiene el historial de transacciones de una cuenta bancaria con paginación.
    /// </summary>
    /// <param name="accountNumber">Número de cuenta</param>
    /// <param name="page">Página actual (por defecto 1)</param>
    /// <param name="pageSize">Cantidad de registros por página (máximo 100)</param>
    /// <param name="type">Filtro opcional por tipo de transacción (Deposit o Withdrawal)</param>
    /// <returns>Lista paginada de transacciones</returns>
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