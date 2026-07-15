using Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Infrastructure.Persistence.Configurations;

public class AccountConfiguration 
    : IEntityTypeConfiguration<BankAccount>
{
    public void Configure(
        EntityTypeBuilder<BankAccount> builder)
    {
        builder.HasKey(x => x.Id);


        builder.Property(x => x.AccountNumber)
            .HasMaxLength(20)
            .IsRequired();


        builder.HasIndex(x => x.AccountNumber)
            .IsUnique();


        builder.Property(x => x.Balance)
            .HasPrecision(18, 2);


        builder.Property(x => x.CreatedAt)
            .IsRequired();


        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}