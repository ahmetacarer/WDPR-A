using Microsoft.AspNetCore.Identity;
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

               
        builder.Entity<IdentityUser>()
               .HasIndex(c => c.Email)
               .IsUnique();

        builder.Entity<Orthopedagogue>().HasData(

            new Orthopedagogue { FirstName = "Karin", LastName = "Kemper", Specialty = "ADHD", PictureUrl = "https://i.postimg.cc/tRPnMpWP/Karin-Kemper-Orthopedagoog.png", OrthopedagogueWebText = File.ReadAllText(@"../src/wwwroot/WebTexts/KarinKemperWebText.html")},

            new Orthopedagogue { FirstName = "Johan", LastName = "Lo", Specialty = "Faalangst", PictureUrl = "https://i.postimg.cc/9fwqH7rm/Johan-Lo-Orthopedagoog.png", OrthopedagogueWebText = File.ReadAllText(@"../src/wwwroot/WebTexts/JohanLoWebText.html") },

            new Orthopedagogue { FirstName = "Steven", LastName = "Ito", Specialty = "Eetstoornis", PictureUrl = "https://i.postimg.cc/bNbyP9RF/Steven-Ito-Orthopedagoog.png", OrthopedagogueWebText = File.ReadAllText(@"../src/wwwroot/WebTexts/StevenItoWebText.html")},

            new Orthopedagogue { FirstName = "Marianne", LastName = "van Dijk", Specialty = "Dyslexie", PictureUrl = "https://i.postimg.cc/wTSpbR8c/Marianne-Van-Dijk-Orthopedagoog.png", OrthopedagogueWebText = File.ReadAllText(@"../src/wwwroot/WebTexts/MarianneVanDijkWebText.html") }

        );
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Guardian> Guardians { get; set; }
    public DbSet<Orthopedagogue> Orthopedagogues { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<User> InheritedUsers { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
}