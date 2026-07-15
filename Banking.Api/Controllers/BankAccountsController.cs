using Banking.Application.DTOs.Account;
using Banking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers;

/// <summary>
/// Manages bank account creation and balance queries.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BankAccountsController(
    IBankAccountService bankAccountService)
    : ControllerBase
{
    /// <summary>
    /// Creates a new bank account for an existing customer.
    /// </summary>
    /// <param name="dto">Bank account creation data including the customer ID and initial balance.</param>
    /// <returns>The newly created bank account.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BankAccountResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BankAccountResponseDto>> Create(
        CreateBankAccountDto dto)
    {
        var account = await bankAccountService.CreateAccountAsync(dto);

        return CreatedAtAction(
            nameof(Create),
            new { id = account.Id },
            account);
    }

    /// <summary>
    /// Retrieves the current balance of a bank account.
    /// </summary>
    /// <param name="accountNumber">The account number to query.</param>
    /// <returns>The account number and its current balance.</returns>
    [HttpGet("{accountNumber}/balance")]
    [ProducesResponseType(typeof(BalanceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BalanceDto>> GetBalance(
      string accountNumber)
    {
        var balance = await bankAccountService
            .GetBalanceAsync(accountNumber);

        return Ok(balance);
    }
}