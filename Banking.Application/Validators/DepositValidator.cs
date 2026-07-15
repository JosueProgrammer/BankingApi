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
            .WithMessage("El monto del depósito debe ser mayor a cero.");
    }
}