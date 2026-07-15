using Banking.Domain.Enums;

namespace Banking.Application.DTOs.Customer;

public class CreateCustomerDto
{
    public string FullName { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    public Gender Gender { get; set; }

    public decimal MonthlyIncome { get; set; }
}