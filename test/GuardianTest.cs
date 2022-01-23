using Xunit;
using Moq;
using System;
using WDPR_A.ViewModels;
using Microsoft.AspNetCore.Mvc;
using WDPR_A.Controllers;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Linq;

namespace test;

public class GuardianTest
{
    [Fact]
    public void Index_RedirectToDashboard_True()
    {
        var context = ManagerContainer.GetWDPRContext();
        var sut = new GuardianController(null, context, null);
        var result = sut.Index();

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Dashboard", redirectToActionResult.ActionName);
    }

    [Fact]
    public void GuardianController_NoCurrentUser_Null()
    {
        var context = ManagerContainer.GetWDPRContext();
        var userManager = ManagerContainer.TestUserManager<IdentityUser>();
        var sut = new GuardianController(null, context, userManager);
        Assert.Null(sut.User);
    }

    public async Task SeedData(WDPRContext context)
    {
        var orthopedagogue = new Orthopedagogue { Id = Guid.NewGuid().ToString(), FirstName = "", LastName = "", Specialty = "ADHD" };
        var client = new Client { Id = "1", FirstName = "aa", LastName = "aa", Address = "", Residence = "", Condition = "ADHD", AgeCategory = AgeCategory.Middelste, Guardians = new List<Guardian> { }, Chats = new List<Chat> { new Chat { Orthopedagogue = orthopedagogue, Messages = new HashSet<Message> { }, RoomId = Guid.NewGuid().ToString() } } };
        var guardian = new Guardian { Id = Guid.NewGuid().ToString(), Clients = new List<Client> { client }, FirstName = "John", LastName = "Doe", UserName = "john@doe.com" };
        var message = new Message { ChatRoomId = client.Chats[0].RoomId, Text = "test bericht", Sender = client };
        context.Add(orthopedagogue);
        context.Add(client);
        context.Add(guardian);
        context.Add(message);
        await context.SaveChangesAsync();
    }

    //guardian can't read messages of client
    [Fact]
    public async Task Dashboard_GuardianWithClientUnderSixteen_FrequencyMessagesAsync()
    {
        var context = ManagerContainer.GetWDPRContext();
        var orthopedagogue = new Orthopedagogue { Id = Guid.NewGuid().ToString(), FirstName = "", LastName = "", Specialty = "ADHD" };
        var client = new Client { Id = "1", FirstName = "aa", LastName = "aa", Address = "", Residence = "", Condition = "ADHD", AgeCategory = AgeCategory.Middelste, Guardians = new List<Guardian> { }, Chats = new List<Chat> { new Chat { Orthopedagogue = orthopedagogue, Messages = new HashSet<Message> { }, RoomId = Guid.NewGuid().ToString() } } };
        var guardian = new Guardian { Id = "100", Clients = new List<Client> { client }, FirstName = "John", LastName = "Doe", UserName = "john@doe.com" };
        var message = new Message { ChatRoomId = client.Chats[0].RoomId, Text = "test bericht", Sender = client };
        context.Add(orthopedagogue);
        context.Add(client);
        context.Add(guardian);
        context.Add(message);
        await context.SaveChangesAsync();

        Assert.NotNull(context.Guardians.First());
        var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "100"),
        }));
        var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(_ => _.GetUserAsync(principal))
            .ReturnsAsync(guardian);
        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal } };

        var sut = new GuardianController(null, context, mockUserManager.Object);
        sut.ControllerContext = controllerContext;
        var action = await sut.Dashboard();
        var viewResult = Assert.IsType<ViewResult>(action);
        var model = Assert.IsType<Guardian>(viewResult.Model);
        Assert.Equal(1, model.Clients[0].Chats.Sum(c => c.Messages.Count()));
    }



}