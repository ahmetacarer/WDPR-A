using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Models;

public class WDPRContext : IdentityDbContext
{
    public WDPRContext(DbContextOptions<WDPRContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Client>()
               .HasIndex(c => c.ChatCode)
               .IsUnique();
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Guardian> Guardians { get; set; }
    public DbSet<Orthopedagogue> Orthopedagogues { get; set; }
    public DbSet<Client> Clients { get; set; }
}