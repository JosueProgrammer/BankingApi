namespace Banking.Domain.Entities;

using Banking.Domain.Enums;
using Banking.Domain.Exceptions;

public class Customer
{
    public Guid Id { get; private set; }

    public string FullName { get; private set; }

    public DateTime BirthDate { get; private set; }

    public Gender Gender { get; private set; }

    public decimal MonthlyIncome { get; private set; }


    private Customer() { }


    public Customer(
        string fullName,
        DateTime birthDate,
        Gender gender,
        decimal monthlyIncome)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new BusinessException(
                "El nombre es obligatorio");


        if (monthlyIncome <= 0)
            throw new BusinessException(
                "Los ingresos deben ser mayores a cero");


        if (birthDate > DateTime.UtcNow)
            throw new BusinessException(
                "Fecha inválida");


        Id = Guid.NewGuid();
        FullName = fullName;
        BirthDate = birthDate;
        Gender = gender;
        MonthlyIncome = monthlyIncome;
    }
}