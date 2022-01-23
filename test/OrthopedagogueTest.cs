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

public class OrthopedagogueTest
{
    public WDPRContext GetWDPRContext()
    {
        var options = new DbContextOptionsBuilder<WDPRContext>().EnableSensitiveDataLogging().
                        UseInMemoryDatabase("MijnDatabase")
                        .Options;
        var context = new WDPRContext(options);
        return context;
    }

    [Fact]
    public async Task OrthopedagogueDashboard_ViewDataTest_AppointmentsFound()
    {
        var context = GetWDPRContext();

        var orthopedagogue = new Orthopedagogue
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Jacob",
            LastName = "Lans",
            Specialty = "Dyslexie",
            EmailConfirmed = true
        };

        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Jordy",
            LastName = "van Dijk",
            Condition = "Dyslexie",
            AgeCategory = AgeCategory.Oudste,
            Guardians = null,
            Chats = null,
            Address = "Street 23",
            Residence = "The Hague",
            IsBlocked = false,
            Email = "dsteen@voorbeeld.nl",
            UserName = "dsteen@voorbeeld.nl",
            EmailConfirmed = false
        };

        var appointment = new Appointment
        {
            Id = 22,
            Orthopedagogue = orthopedagogue,
            IncomingClient = client,
            IsVerified = false
        };

        context.Orthopedagogues.Add(orthopedagogue);
        context.Clients.Add(client);
        context.Appointments.Add(appointment);
        await context.SaveChangesAsync();


        var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, orthopedagogue.Id),
        }));
        var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(_ => _.GetUserAsync(principal))
            .ReturnsAsync(orthopedagogue);

        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal } };

        var sut = new OrthopedagogueController(null, context, mockUserManager.Object);
        sut.ControllerContext = controllerContext;
        var action = await sut.Dashboard();

        var viewResult = Assert.IsType<ViewResult>(action);

        Assert.Null(viewResult.ViewData["Melding"]);

        var count = Assert.IsType<List<Appointment>>(viewResult.Model).Count;
        Assert.True(count == 1);
    }

    [Fact]
    public async Task OrthopedagogueDashboard_ViewDataTest_NoAppointmentsFound()
    {
        var context = GetWDPRContext();

        var orthopedagogue = new Orthopedagogue
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Jacob",
            LastName = "Lans",
            Specialty = "Dyslexie",
            EmailConfirmed = true
        };

        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
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
            EmailConfirmed = false
        };

        var appointment = new Appointment
        {
            Id = 23,
            Orthopedagogue = orthopedagogue,
            IncomingClient = client,
            IsVerified = true
        };

        context.Orthopedagogues.Add(orthopedagogue);
        context.Clients.Add(client);
        context.Appointments.Add(appointment);
        await context.SaveChangesAsync();


        var principal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, orthopedagogue.Id),
        }));
        var mockUserStore = new Mock<IUserStore<IdentityUser>>();
        var mockUserManager = new Mock<UserManager<IdentityUser>>(mockUserStore.Object, null, null, null, null, null, null, null, null);
        mockUserManager
            .Setup(_ => _.GetUserAsync(principal))
            .ReturnsAsync(orthopedagogue);

        var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal } };

        var sut = new OrthopedagogueController(null, context, mockUserManager.Object);
        sut.ControllerContext = controllerContext;
        var action = await sut.Dashboard();

        var viewResult = Assert.IsType<ViewResult>(action);

        Assert.Equal("Er zijn momenteel geen afspraken ingepland.", viewResult.ViewData["Melding"].ToString());

        var count = Assert.IsType<List<Appointment>>(viewResult.Model).Count;
        Assert.True(count == 0);
    }
}