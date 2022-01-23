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

        builder.Entity<IdentityRole>().HasData(
            GetRoles()
         );

        builder.Entity<Orthopedagogue>().HasData(
            GetOrthopedagogues() 
        );

        builder.Entity<IdentityUserRole<string>>().HasData(
            GetOrthopedagoguesWithRoles()
        );
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }


    public List<IdentityRole> GetRoles()
    {
        return new List<IdentityRole> {
            new IdentityRole { Id = "006c921b-611f-4cf9-a18c-3d722840ee42", Name = "Orthopedagogue", NormalizedName = "Orthopedagogue".ToUpper()},
            new IdentityRole { Id = "26d89e3f-e47f-468b-bd78-6f58aef3285e", Name = "Moderator", NormalizedName = "Moderator".ToUpper()},
            new IdentityRole { Id = "6e7321be-b891-44a3-abd8-71283880ddb9", Name = "Client", NormalizedName = "Client".ToUpper()},
            new IdentityRole { Id = "ede8d0e5-2581-476c-ba45-a99d7089844a", Name = "Guardian", NormalizedName = "Guardian".ToUpper()}
        };
    }


    public List<Orthopedagogue> GetOrthopedagogues()
    {
        return new List<Orthopedagogue> {
            new Orthopedagogue { Id = "13198681-ef68-4acf-bd7e-544e12fed291", FirstName = "Karin", LastName = "Kemper", Specialty = "ADHD", NormalizedEmail = "KKEMPER@ZMDH.NL", Email = "kkemper@zmdh.nl", NormalizedUserName = "KKEMPER@ZMDH.NL", UserName = "kkemper@zmdh.nl", PasswordHash = "AQAAAAEAACcQAAAAECliP0eZF/dtPcZTjNEfC7Sh+XjlLTW0LhuATCboH6s/1GZZsLvr9LiQEpMOLZ7pQA==" },

            new Orthopedagogue { Id = "7d028f6c-929e-45b0-8493-573078b85f79", FirstName = "Johan", LastName = "Lo", Specialty = "Faalangst", NormalizedEmail = "JLO@ZMDH.NL", Email = "jlo@zmdh.nl", NormalizedUserName = "JLO@ZMDH.NL", UserName = "jlo@zmdh.nl", PasswordHash = "AQAAAAEAACcQAAAAECliP0eZF/dtPcZTjNEfC7Sh+XjlLTW0LhuATCboH6s/1GZZsLvr9LiQEpMOLZ7pQA==" },

            new Orthopedagogue { Id = "1988e216-9179-42a1-8243-2b6bf362b1b4", FirstName = "Steven", LastName = "Ito", Specialty = "Eetstoornis", NormalizedEmail = "SITO@ZMDH.NL", Email = "sito@zmdh.nl", NormalizedUserName = "SITO@ZMDH.NL", UserName = "sito@zmdh.nl", PasswordHash = "AQAAAAEAACcQAAAAECliP0eZF/dtPcZTjNEfC7Sh+XjlLTW0LhuATCboH6s/1GZZsLvr9LiQEpMOLZ7pQA==" },

            new Orthopedagogue { Id = "4e3371ca-b20a-4c91-b6c2-7c872c310a54", FirstName = "Marianne", LastName = "van Dijk", Specialty = "Dyslexie", NormalizedEmail = "MVDIJK@ZMDH.NL", Email = "mvdijk@zmdh.nl", NormalizedUserName = "MVDIJK@ZMDH.NL", UserName = "mvdijk@zmdh.nl", PasswordHash = "AQAAAAEAACcQAAAAECliP0eZF/dtPcZTjNEfC7Sh+XjlLTW0LhuATCboH6s/1GZZsLvr9LiQEpMOLZ7pQA==" }
        };
    }

    public List<IdentityUserRole<string>> GetOrthopedagoguesWithRoles()
    {
        var moderatorRole = GetRoles().SingleOrDefault(r => r.Name == "Moderator");
        var orthopedagogueRole = GetRoles().SingleOrDefault(r => r.Name == "Orthopedagogue");
        var orthopedagoguesWithRoles = new List<IdentityUserRole<string>>();
        foreach (var orthopedagogue in GetOrthopedagogues())
        {
            if (moderatorRole != null)
                orthopedagoguesWithRoles.Add(new IdentityUserRole<string>(){ UserId = orthopedagogue.Id, RoleId = moderatorRole.Id });
            if (orthopedagogueRole != null)
                orthopedagoguesWithRoles.Add(new IdentityUserRole<string>(){ UserId = orthopedagogue.Id, RoleId = orthopedagogueRole.Id });
        }
        return orthopedagoguesWithRoles;
    }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Guardian> Guardians { get; set; }
    public DbSet<Orthopedagogue> Orthopedagogues { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<User> InheritedUsers { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
}