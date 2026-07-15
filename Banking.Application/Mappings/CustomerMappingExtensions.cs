using Banking.Application.DTOs.Customer;
using Banking.Domain.Entities;

namespace Banking.Application.Mappings;

public static class CustomerMappingExtensions
{
    public static CustomerResponseDto ToResponseDto(
        this Customer customer)
    {
        return new CustomerResponseDto
        {
            Id = customer.Id,
            FullName = customer.FullName,
            BirthDate = customer.BirthDate,
            Gender = customer.Gender,
            MonthlyIncome = customer.MonthlyIncome
        };
    }
}