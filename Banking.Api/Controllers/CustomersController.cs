using Banking.Application.DTOs.Customer;
using Banking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController(
    ICustomerService customerService)
    : ControllerBase
{

    [HttpPost]
    public async Task<ActionResult<CustomerResponseDto>> CreateCustomer(
        CreateCustomerDto dto)
    {
        var customer = await customerService
            .CreateCustomerAsync(dto);

        return CreatedAtAction(
            nameof(GetCustomerById),
            new { id = customer.Id },
            customer);
    }


    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerResponseDto>> GetCustomerById(
        Guid id)
    {
        var customer = await customerService
            .GetCustomerByIdAsync(id);

        return Ok(customer);
    }
}