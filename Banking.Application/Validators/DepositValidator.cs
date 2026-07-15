using Banking.Application.DTOs.Transaction;
using FluentValidation;

namespace Banking.Application.Validators;

public class DepositValidator 
    : AbstractValidator<DepositDto>
{
    public DepositValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("The deposit amount must be greater than zero.");
    }
}