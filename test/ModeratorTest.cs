
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
    //15

    [Fact]
    public async Task BlockClient_BlocktheClient_True()
    {
        DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
        WDPRContext WDPRContext = new WDPRContext(options);

        var controller = new ModeratorController(null, WDPRContext, GuardianTest.TestUserManager<IdentityUser>());

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
        WDPRContext.Clients.Add(client);
        await WDPRContext.SaveChangesAsync();

        await controller.BlockClient(client.Id);

        Assert.True(client.IsBlocked);
    }

    //16

    [Fact]
    public async Task UnBlockClient_UnBlocktheClient_False()
    {
        DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
        WDPRContext WDPRContext = new WDPRContext(options);

        var controller = new ModeratorController(null, WDPRContext, GuardianTest.TestUserManager<IdentityUser>());

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
        WDPRContext.Clients.Add(client);
        await WDPRContext.SaveChangesAsync();

        await controller.UnblockClient(client.Id);

        Assert.False(client.IsBlocked);
    }
}