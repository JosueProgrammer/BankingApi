using Banking.Domain.Enums;

namespace Banking.Application.DTOs.Customer;

/// <summary>
/// Response payload representing a customer.
/// </summary>
public class CustomerResponseDto
{
    /// <summary>The customer's unique identifier.</summary>
    public Guid Id { get; set; }

    /// <summary>The customer's full name.</summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>The customer's date of birth.</summary>
    public DateTime BirthDate { get; set; }

    /// <summary>The customer's gender.</summary>
    public Gender Gender { get; set; }

    /// <summary>The customer's monthly income.</summary>
    public decimal MonthlyIncome { get; set; }
}