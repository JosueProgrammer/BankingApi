using Banking.Application.DTOs.Customer;
using Banking.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Api.Controllers;

/// <summary>
/// Manages customer registration and retrieval.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomersController(
    ICustomerService customerService)
    : ControllerBase
{

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="dto">Customer creation data.</param>
    /// <returns>The newly created customer.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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


    /// <summary>
    /// Retrieves a customer by their unique identifier.
    /// </summary>
    /// <param name="id">The customer's unique identifier (GUID).</param>
    /// <returns>The customer details.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CustomerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CustomerResponseDto>> GetCustomerById(
        Guid id)
    {
        var customer = await customerService
            .GetCustomerByIdAsync(id);

        return Ok(customer);
    }
}