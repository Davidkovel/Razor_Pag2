using Microsoft.EntityFrameworkCore;
using Razor_Pags_2.Features.Clients.Models;

namespace Razor_Pags_2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    // Clients
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Phone> Phones => Set<Phone>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<FinanceAccount> FinanceAccounts => Set<FinanceAccount>();
    public DbSet<ClientFinanceAccount> ClientFinanceAccounts => Set<ClientFinanceAccount>();

    // Users
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Status> Statuses => Set<Status>();
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Client>()
            .HasIndex(b => b.Email)
            .IsUnique();
        
        modelBuilder.Entity<ClientFinanceAccount>()
            .HasKey(cfa => new { cfa.ClientId, cfa.FinanceAccountId });
    }
}