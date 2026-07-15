using Banking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Banking.Infrastructure.Persistence;

public class BankingDbContext(
    DbContextOptions<BankingDbContext> options)
    : DbContext(options)
{
    public DbSet<Customer> Customers { get; set; }


    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BankingDbContext).Assembly);
    }
}