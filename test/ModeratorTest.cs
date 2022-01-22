
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Controllers;
using WDPR_A.Models;
using Xunit;

namespace test;

public class ModeratorTest
{

    public WDPRContext GetWDPRContext()
    {
        var options = new DbContextOptionsBuilder<WDPRContext>().EnableSensitiveDataLogging().
                        UseInMemoryDatabase("MijnDatabase")
                        .Options;
        var context = new WDPRContext(options);
        return context;
    }

    //15

    [Fact]
    public async Task BlockClient_BlocktheClient_True()
    {
        var context = GetWDPRContext();
        var controller = new ModeratorController(null, context, GuardianTest.TestUserManager<IdentityUser>());

        var client = new Client
        {
            Id = "1",
            FirstName = "Henkie",
            LastName = "Penkie",
            Condition = "ADHD",
            AgeCategory = AgeCategory.Oudste,
            Guardians = null,
            Chats = null,
            Address = "Street 23",
            Residence = "The Hague",
            IsBlocked = false
        };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        await controller.BlockClient(client.Id);

        Assert.True(client.IsBlocked);
    }

    //16

    [Fact]
    public async Task UnBlockClient_UnBlocktheClient_False()
    {
        var context = GetWDPRContext();

        var controller = new ModeratorController(null, context, GuardianTest.TestUserManager<IdentityUser>());

        var client = new Client
        {
            Id = "2",
            FirstName = "Henkie",
            LastName = "Penkie",
            Condition = "ADHD",
            AgeCategory = AgeCategory.Oudste,
            Guardians = null,
            Chats = null,
            Address = "Street 23",
            Residence = "The Hague",
            IsBlocked = false
        };
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        await controller.BlockClient(client.Id);

        await controller.UnblockClient(client.Id);

        Assert.False(client.IsBlocked);
    }
}