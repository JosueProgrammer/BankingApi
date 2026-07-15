using Banking.Application.DTOs.Transaction;
using FluentValidation;

namespace Banking.Application.Validators;

public class WithdrawValidator 
    : AbstractValidator<WithdrawDto>
{
    public WithdrawValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("The withdrawal amount must be greater than zero.");
    }
}