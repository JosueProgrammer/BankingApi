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
            .WithMessage("Customer ID is required.");

        RuleFor(x => x.InitialBalance)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Initial balance cannot be negative.");
    }
}