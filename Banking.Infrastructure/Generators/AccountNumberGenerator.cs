using Banking.Domain.Interfaces;

namespace Banking.Infrastructure.Generators;

public class AccountNumberGenerator 
    : IAccountNumberGenerator
{
    public string Generate()
    {
        var date = DateTime.UtcNow
            .ToString("yyyyMMdd");


        var random = Random.Shared
            .Next(0, 9999)
            .ToString("D4");


        return $"ACC-{date}-{random}";
    }
}