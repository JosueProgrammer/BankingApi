using Banking.Application.Interfaces;
using Banking.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Banking.Application.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}