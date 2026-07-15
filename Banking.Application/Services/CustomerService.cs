using Banking.Application.DTOs.Customer;
using Banking.Application.Interfaces;
using Banking.Application.Mappings;
using Banking.Domain.Entities;
using Banking.Domain.Exceptions;
using Banking.Domain.Interfaces;

namespace Banking.Application.Services;

public class CustomerService(ICustomerRepository customerRepository) 
    : ICustomerService
{
    public async Task<CustomerResponseDto> CreateCustomerAsync(
        CreateCustomerDto dto)
    {
        var customer = new Customer(
            dto.FullName,
            dto.BirthDate,
            dto.Gender,
            dto.MonthlyIncome
        );

        var createdCustomer = await customerRepository
            .CreateAsync(customer);

        return createdCustomer.ToResponseDto();
    }


    public async Task<CustomerResponseDto> GetCustomerByIdAsync(
        Guid id)
    {
        var customer = await customerRepository.GetByIdAsync(id)
            ?? throw new NotFoundException(
                "El cliente no fue encontrado");

        return customer.ToResponseDto();
    }
}