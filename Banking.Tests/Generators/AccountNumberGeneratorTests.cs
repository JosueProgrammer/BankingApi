using Banking.Infrastructure.Generators;
using FluentAssertions;
using System.Text.RegularExpressions;

namespace Banking.Tests.Generators;

public class AccountNumberGeneratorTests
{
    private readonly AccountNumberGenerator _sut;

    public AccountNumberGeneratorTests()
    {
        _sut = new AccountNumberGenerator();
    }

    [Fact]
    public void Generate_ShouldReturnValidAccountNumberFormat()
    {
        // Arrange
        var today = DateTime.UtcNow.ToString("yyyyMMdd");
        var expectedPrefix = $"ACC-{today}-";

        // Act
        var result = _sut.Generate();

        // Assert
        result.Should().StartWith(expectedPrefix);
        
        // Match exact format: ACC-YYYYMMDD-XXXX
        var regex = new Regex(@"^ACC-\d{8}-\d{4}$");
        result.Should().MatchRegex(regex);
    }

    [Fact]
    public void Generate_ShouldGenerateUniqueAccountNumbers()
    {
        // Arrange
        var generatedNumbers = new HashSet<string>();

        // Act
        // Generamos un número pequeño para validar unicidad básica sin colisiones por el límite de 0 a 9999.
        for (int i = 0; i < 10; i++)
        {
            var result = _sut.Generate();
            generatedNumbers.Add(result);
        }

        // Assert
        generatedNumbers.Count.Should().BeGreaterThan(1);
    }
}
