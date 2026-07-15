using Banking.Application.DTOs.Account;
using Banking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankAccountsController(
    IBankAccountService bankAccountService)
    : ControllerBase
{
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