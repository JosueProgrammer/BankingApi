using Banking.Application.DTOs.Account;
using FluentValidation;

namespace Banking.Application.Validators.Account;

public class CreateBankAccountValidator
    : AbstractValidator<CreateBankAccountDto>
{
    public CreateBankAccountValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("El cliente es obligatorio.");

        RuleFor(x => x.InitialBalance)
            .GreaterThanOrEqualTo(0)
            .WithMessage("El saldo inicial no puede ser negativo.");
    }
}