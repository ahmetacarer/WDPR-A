using System;
using System.Collections.Generic;
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
public class ChatControllerTest
{

    public async Task SeedData(WDPRContext context)
    {
        var client = new Client
        {
            Id = "1",
            AgeCategory = AgeCategory.Oudste,
            FirstName = "",
            LastName = "",
            Condition = "ADHD",
            Address = "",
            Residence = ""
        };
        var orthopedagogue = new Orthopedagogue
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "",
            LastName = "",
            Specialty = "ADHD"
        };
        var chat = new Chat
        {
            RoomId = Guid.NewGuid().ToString(),
            Orthopedagogue = orthopedagogue,
            Clients = new List<Client> { client }
        };
        var message = new Message
        {
            Sender = client,
            Text = "slur",
            When = DateTime.Now,
            ChatRoomId = chat.RoomId
        };
        await context.Clients.AddAsync(client);
        await context.Orthopedagogues.AddAsync(orthopedagogue);
        await context.Chats.AddAsync(chat);
        await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();

    }
    [Fact]
    public async Task Index_ClientWithOneChat_ReturnsOneChatInListAsync()
    {
        var context = ManagerContainer.GetWDPRContext();
        await SeedData(context);
        var testedClient = await context.Clients.FirstAsync();
        var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, testedClient.Id),
            new Claim(ClaimTypes.Role, "Client")
        }));
        var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(_ => _.GetUserAsync(principal))
            .ReturnsAsync(testedClient);
        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal } };

        var sut = new ChatController(null, context, mockUserManager.Object);
        sut.ControllerContext = controllerContext;

        var action = await sut.Index();
        var viewResult = Assert.IsType<ViewResult>(action);
        var model = Assert.IsType<List<Chat>>(viewResult.Model);
        Assert.True(model.Count == 1);
    }


    [Fact]
    public async Task Index_OrthopedagogueWithOneChat_ReturnsOneChatInListAsync()
    {
        var context = ManagerContainer.GetWDPRContext();
        await SeedData(context);
        var fakeOrthopedagogue = await context.Orthopedagogues.FirstAsync();
        var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, fakeOrthopedagogue.Id),
            new Claim(ClaimTypes.Role, "Orthopedagogue", "Moderator")
        }));
        var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(_ => _.GetUserAsync(principal))
            .ReturnsAsync(fakeOrthopedagogue);
        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal } };

        var sut = new ChatController(null, context, mockUserManager.Object);
        sut.ControllerContext = controllerContext;

        var action = await sut.Index();
        var viewResult = Assert.IsType<ViewResult>(action);
        var model = Assert.IsType<List<Chat>>(viewResult.Model);
        Assert.True(model.Count == 1);
    }

    [Fact]
    public async Task ReportClient_ReportsAnonymously_MessageReportCountGoesUp()
    {
        var context = ManagerContainer.GetWDPRContext();
        await SeedData(context);

        var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        var sut = new ChatController(null, context, mockUserManager.Object);

        var message = await context.Messages.FirstAsync();
        Assert.Equal(0, message.ReportCount);
        sut.ReportClient(message.Id);
        Assert.Equal(1, message.ReportCount);
    }
}