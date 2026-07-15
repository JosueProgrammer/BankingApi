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
            .WithMessage("El nombre completo es obligatorio");


        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.UtcNow)
            .WithMessage("La fecha de nacimiento no es válida");


        RuleFor(x => x.MonthlyIncome)
            .GreaterThan(0)
            .WithMessage("Los ingresos deben ser mayores a cero");
    }
}