using Banking.Application.DTOs.Customer;
using FluentValidation;

namespace Banking.Application.Validators;

public class CreateCustomerValidator 
    : AbstractValidator<CreateCustomerDto>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.");


        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.UtcNow)
            .WithMessage("Birth date is not valid.");


        RuleFor(x => x.MonthlyIncome)
            .GreaterThan(0)
            .WithMessage("Monthly income must be greater than zero.");
    }
}