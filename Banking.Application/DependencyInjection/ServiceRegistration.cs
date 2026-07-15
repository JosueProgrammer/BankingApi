using Banking.Application.Interfaces;
using Banking.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace Banking.Application.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();

        services.AddScoped<IBankAccountService, BankAccountService>();

        services.AddValidatorsFromAssembly(typeof(ServiceRegistration).Assembly);
        return services;
    }
}