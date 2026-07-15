using Banking.Domain.Enums;

namespace Banking.Application.DTOs.Customer;

public class CreateCustomerDto
{
    /// <example>Juan Pérez</example>
    public string FullName { get; set; } = string.Empty;

    /// <example>1995-05-20</example>
    public DateTime BirthDate { get; set; }

    /// <example>Male</example>
    public Gender Gender { get; set; }

    /// <example>2500.00</example>
    public decimal MonthlyIncome { get; set; }
}