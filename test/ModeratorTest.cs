using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Controllers;
using WDPR_A.Models;
using Xunit;

namespace test;

public class ModeratorTest
{
    [Fact]
    public async Task BlockClient_BlocktheClient_True()
    {
        var context = ManagerContainer.GetWDPRContext();
        var controller = new ModeratorController(null, context, ManagerContainer.TestUserManager<IdentityUser>());

        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
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

    [Fact]
    public async Task UnBlockClient_UnBlocktheClient_False()
    {
        var context = ManagerContainer.GetWDPRContext();

        var controller = new ModeratorController(null, context, ManagerContainer.TestUserManager<IdentityUser>());

        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
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

    [Fact]
    public async Task ModeratorDashboard_ViewDataTest_SearchResultFound()
    {
        var context = ManagerContainer.GetWDPRContext();

        var controller = new ModeratorController(null, context, ManagerContainer.TestUserManager<IdentityUser>());

        var orthopedagogue = new Orthopedagogue
        {
            Id = "54545454223",
            FirstName = "Jacob",
            LastName = "Lans",
            Specialty = "Dyslexie",
        };

        var client = new Client
        {
            Id = "54554445",
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

        var appointment = new Appointment
        {
            Id = 1,
            Orthopedagogue = orthopedagogue,
            IncomingClient = client,
            IsVerified = true
        };

        context.Orthopedagogues.Add(orthopedagogue);
        context.Clients.Add(client);
        context.Appointments.Add(appointment);
        await context.SaveChangesAsync();

        var result = await controller.Dashboard("Dennis");
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Null(viewResult.ViewData["Melding"]);

        var count = Assert.IsType<List<Appointment>>(viewResult.Model).Count;
        Assert.True(count == 1);
    }

    [Fact]
    public async Task ModeratorDashboard_ViewDataTest_NoSearchResultFound()
    {
        var context = ManagerContainer.GetWDPRContext();

        var controller = new ModeratorController(null, context, ManagerContainer.TestUserManager<IdentityUser>());

        var orthopedagogue = new Orthopedagogue
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Jacob",
            LastName = "Lans",
            Specialty = "Dyslexie",
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
        };

        context.Orthopedagogues.Add(orthopedagogue);
        context.Clients.Add(client);
        await context.SaveChangesAsync();

        var result = await controller.Dashboard("EenOnvindbareNaam");
        var viewResult = Assert.IsType<ViewResult>(result);

        Assert.Equal("Er zijn geen behandelingen gevonden.", viewResult.ViewData["Melding"].ToString());

        var count = Assert.IsType<List<Appointment>>(viewResult.Model).Count;
        Assert.True(count == 0);
    }
}