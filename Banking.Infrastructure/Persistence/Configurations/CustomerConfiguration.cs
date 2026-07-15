using Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration 
    : IEntityTypeConfiguration<Customer>
{
    public void Configure(
        EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);


        builder.Property(x => x.FullName)
            .HasMaxLength(100)
            .IsRequired();


        builder.Property(x => x.MonthlyIncome)
            .HasPrecision(18, 2);
    }
}