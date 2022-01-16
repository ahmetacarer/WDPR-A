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
        builder.Entity<Orthopedagogue>().HasData(

            new Orthopedagogue { FirstName = "Karin", LastName = "Kemper", Specialty = "ADHD", NormalizedEmail = "KKEMPER@ZMDH.NL", Email = "kkemper@zmdh.nl", NormalizedUserName = "KKEMPER@ZMDH.NL", UserName = "kkemper@zmdh.nl", PasswordHash = "AQAAAAEAACcQAAAAECliP0eZF/dtPcZTjNEfC7Sh+XjlLTW0LhuATCboH6s/1GZZsLvr9LiQEpMOLZ7pQA==" },

            new Orthopedagogue { FirstName = "Johan", LastName = "Lo", Specialty = "Faalangst", NormalizedEmail = "JLO@ZMDH.NL", Email = "jlo@zmdh.nl", NormalizedUserName = "JLO@ZMDH.NL", UserName = "jlo@zmdh.nl", PasswordHash = "AQAAAAEAACcQAAAAECliP0eZF/dtPcZTjNEfC7Sh+XjlLTW0LhuATCboH6s/1GZZsLvr9LiQEpMOLZ7pQA==" },

            new Orthopedagogue { FirstName = "Steven", LastName = "Ito", Specialty = "Eetstoornis", NormalizedEmail = "SITO@ZMDH.NL", Email = "sito@zmdh.nl", NormalizedUserName = "SITO@ZMDH.NL", UserName = "sito@zmdh.nl", PasswordHash = "AQAAAAEAACcQAAAAECliP0eZF/dtPcZTjNEfC7Sh+XjlLTW0LhuATCboH6s/1GZZsLvr9LiQEpMOLZ7pQA==" },

            new Orthopedagogue { FirstName = "Marianne", LastName = "van Dijk", Specialty = "Dyslexie", NormalizedEmail = "MVDIJK@ZMDH.NL", Email = "mvdijk@zmdh.nl", NormalizedUserName = "MVDIJK@ZMDH.NL", UserName = "mvdijk@zmdh.nl", PasswordHash = "AQAAAAEAACcQAAAAECliP0eZF/dtPcZTjNEfC7Sh+XjlLTW0LhuATCboH6s/1GZZsLvr9LiQEpMOLZ7pQA==" }
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