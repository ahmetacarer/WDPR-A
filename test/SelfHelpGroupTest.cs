using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WDPR_A.Controllers;
using WDPR_A.Models;
using Xunit;
namespace test;

public class SelfHelpGroupTest
{

    [Fact]
    public async Task Create_CreateSelfHelpGroup_True()
    {
        var context = ManagerContainer.GetWDPRContext();
        var generate = ManagerContainer.GetGenerate();
        var chatManager = ManagerContainer.GetChatManager(generate, context);




        var orthopedagogue = new Orthopedagogue
        {
            Id = "100",
            FirstName = "Lansey",
            LastName = "Jordan",
            Specialty = "Dyslexie",
        };

        var client = new Client
        {
            Id = "1",
            FirstName = "Dennis",
            LastName = "Steen",
            Condition = "Dyslexie",
            AgeCategory = AgeCategory.Oudste,
            Guardians = null,
            Chats = null,
            Address = "Street 23",
            Residence = "The Hague",
            IsBlocked = false,
            Email = "dsteen@voorbeeld.nl",
            UserName = "dsteen@voorbeeld.nl",
        };

        context.Orthopedagogues.Add(orthopedagogue);
        context.Clients.Add(client);

        await context.SaveChangesAsync();

        var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
                    new Claim(ClaimTypes.NameIdentifier, "100"),
                    new Claim(ClaimTypes.Role, "Orthopedagogue")
        }));

        var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(_ => _.GetUserAsync(principal))
            .ReturnsAsync(orthopedagogue);

        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal } };


        var sut = new SelfHelpGroupController(null, context, mockUserManager.Object, chatManager);
        sut.ControllerContext = controllerContext;

        await sut.Create("Dyslexie", AgeCategory.Oudste);


        Assert.True(context.Chats.Where(c => !c.IsPrivate).Count() == 1);
    }

    [Fact]
    public async Task SelfHelpGroupIndex_SearchForSelfHelpGroup_ClientFoundResults()
    {
        var context = ManagerContainer.GetWDPRContext();

        var orthopedagogue = new Orthopedagogue { Id = Guid.NewGuid().ToString(), FirstName = "Marianne", LastName = "Dijksma", Specialty = "Dyslexie", EmailConfirmed = true };

        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Rico",
            LastName = "Jong",
            Condition = "Dyslexie",
            AgeCategory = AgeCategory.Middelste,
            Guardians = null,
            Chats = null,
            Address = "Street 23",
            Residence = "The Hague",
            IsBlocked = false,
            Email = "rjong@voorbeeld.nl",
            UserName = "rjong@voorbeeld.nl",
            EmailConfirmed = true,
        };

        var selfhelpgroup1 = new Chat
        {
            IsPrivate = false,
            Subject = "Voetbal",
            Condition = "Dyslexie",
            RoomId = Guid.NewGuid().ToString(),
            Orthopedagogue = orthopedagogue,
            AgeCategory = AgeCategory.Middelste
        };

        var selfhelpgroup2 = new Chat
        {
            IsPrivate = false,
            Subject = "Zwemmen",
            Condition = "Dyslexie",
            RoomId = Guid.NewGuid().ToString(),
            Orthopedagogue = orthopedagogue,
            AgeCategory = AgeCategory.Middelste
        };

        context.Orthopedagogues.Add(orthopedagogue);
        context.Clients.Add(client);
        context.Chats.AddRange(selfhelpgroup1, selfhelpgroup2);
        await context.SaveChangesAsync();


        var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, client.Id),
            new Claim(ClaimTypes.Role, "Client")
        }));
        var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(_ => _.GetUserAsync(principal))
            .ReturnsAsync(client);

        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal } };

        var generate = ManagerContainer.GetGenerate();
        var chatManager = ManagerContainer.GetChatManager(generate, context);

        var sut = new SelfHelpGroupController(null, context, mockUserManager.Object, chatManager);
        sut.ControllerContext = controllerContext;
        var action = await sut.Index("Voet", null);

        var viewResult = Assert.IsType<ViewResult>(action);
        Assert.Null(viewResult.ViewData["Melding"]);

        var count = Assert.IsType<List<Chat>>(viewResult.Model).Count;
        Assert.True(count == 1);
    }

    [Fact]
    public async Task SelfHelpGroupIndex_SearchForSelfHelpGroup_ClientNoFoundResults()
    {
        var context = ManagerContainer.GetWDPRContext();

        var orthopedagogue = new Orthopedagogue { Id = Guid.NewGuid().ToString(), FirstName = "Marianne", LastName = "Dijksma", Specialty = "Dyslexie", EmailConfirmed = true };

        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Rico",
            LastName = "Jong",
            Condition = "Dyslexie",
            AgeCategory = AgeCategory.Middelste,
            Guardians = null,
            Chats = null,
            Address = "Street 23",
            Residence = "The Hague",
            IsBlocked = false,
            Email = "rjong@voorbeeld.nl",
            UserName = "rjong@voorbeeld.nl",
            EmailConfirmed = true,
        };

        var selfhelpgroup1 = new Chat
        {
            IsPrivate = false,
            Subject = "Voetbal",
            Condition = "Dyslexie",
            RoomId = Guid.NewGuid().ToString(),
            Orthopedagogue = orthopedagogue,
            AgeCategory = AgeCategory.Middelste
        };

        var selfhelpgroup2 = new Chat
        {
            IsPrivate = false,
            Subject = "Zwemmen",
            Condition = "Dyslexie",
            RoomId = Guid.NewGuid().ToString(),
            Orthopedagogue = orthopedagogue,
            AgeCategory = AgeCategory.Middelste
        };

        context.Orthopedagogues.Add(orthopedagogue);
        context.Clients.Add(client);
        context.Chats.AddRange(selfhelpgroup1, selfhelpgroup2);
        await context.SaveChangesAsync();


        var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, client.Id),
            new Claim(ClaimTypes.Role, "Client")
        }));
        var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(_ => _.GetUserAsync(principal))
            .ReturnsAsync(client);

        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal } };

        var generate = ManagerContainer.GetGenerate();
        var chatManager = ManagerContainer.GetChatManager(generate, context);

        var sut = new SelfHelpGroupController(null, context, mockUserManager.Object, chatManager);
        sut.ControllerContext = controllerContext;
        var action = await sut.Index("Ssasdda", null);

        var viewResult = Assert.IsType<ViewResult>(action);

        Assert.Equal("Er zijn helaas geen chats gevonden.", viewResult.ViewData["Melding"]);

        var count = Assert.IsType<List<Chat>>(viewResult.Model).Count;
        Assert.True(count == 0);
    }
}