using Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Banking.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration
    : IEntityTypeConfiguration<Transaction>
{
    public void Configure(
        EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);


        builder.Property(x => x.Amount)
            .HasPrecision(18, 2);

        builder.Property(x => x.BalanceAfterTransaction)
            .HasPrecision(18, 2);


        builder.Property(x => x.Type)
            .IsRequired();


        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.IdempotencyKey)
    .HasMaxLength(100)
    .IsRequired();


        builder.HasIndex(x => x.IdempotencyKey)
            .IsUnique();


        builder.HasOne<BankAccount>()
            .WithMany()
            .HasForeignKey(x => x.BankAccountId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}