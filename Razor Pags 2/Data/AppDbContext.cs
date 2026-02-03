using Microsoft.EntityFrameworkCore;
using Razor_Pags_2.Features.Clients.Models;

namespace Razor_Pags_2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Client> Client => Set<Client>();
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Client>()
            .HasIndex(b => b.Email)
            .IsUnique();
    }
}