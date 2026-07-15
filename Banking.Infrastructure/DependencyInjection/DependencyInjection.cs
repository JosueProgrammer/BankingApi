using Banking.Domain.Interfaces;
using Banking.Infrastructure.Persistence;
using Banking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Banking.Infrastructure.Generators;

namespace Banking.Infrastructure.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BankingDbContext>(options =>
        {
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection"));
        });


        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        services.AddScoped<IAccountNumberGenerator, AccountNumberGenerator>();

        return services;
    }
}