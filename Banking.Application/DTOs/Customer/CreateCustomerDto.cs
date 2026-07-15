using Banking.Domain.Enums;

namespace Banking.Application.DTOs.Customer;

/// <summary>
/// Request payload for creating a new customer.
/// </summary>
public class CreateCustomerDto
{
    /// <summary>The customer's full name.</summary>
    /// <example>John Smith</example>
    public string FullName { get; set; } = string.Empty;

    /// <summary>The customer's date of birth.</summary>
    /// <example>1995-05-20</example>
    public DateTime BirthDate { get; set; }

    /// <summary>The customer's gender.</summary>
    /// <example>Male</example>
    public Gender Gender { get; set; }

    /// <summary>The customer's monthly income.</summary>
    /// <example>2500.00</example>
    public decimal MonthlyIncome { get; set; }
}