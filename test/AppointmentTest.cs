using Xunit;
using Moq;
using System;
using WDPR_A.ViewModels;
using WDPR_A.Models;
using WDPR_A.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace test;

public class AppointmentTest
{
    //NameOfMethod_Scenario_Expected

    [Fact]
    public void MakeFirstNameCapitalLetter_MakeCapitalLetter_True()
    {

        var context = ManagerContainer.GetWDPRContext();

        //Arrange
        var controller = new AppointmentController(null, context, null, null);

        var firstName = "hAns";

        //Act
        var result = controller.MakeFirstNameCapitalLetter(firstName);


        //Assert
        var viewResult = Assert.IsType<string>(result);
        Assert.Equal("Hans", result);
    }

    [Fact]
    public void Index_WithParent_NotNull()
    {

        var context = ManagerContainer.GetWDPRContext();

        //Arrange
        var sut = new AppointmentController(null, context, null, null);

        var client = new Client { FirstName = "Hansie", LastName = "Bassie", Email = "testEmail@gmail.com", Condition = "ADHD", AgeCategory = AgeCategory.Jongste };

        //Act
        sut.Index(client, DateTime.Now.Date, DateTime.Parse(DateTime.Now.ToString("hh:mm")), "Ouder@email.com");


        //Assert
        Assert.NotNull(context.Appointments.Where(c => c.IncomingClientId == client.Id).Select(g => g.Guardians));
    }

    [Fact]
    public void Index_WithoutParent_Zero()
    {

        var context = ManagerContainer.GetWDPRContext();

        //Arrange
        var expected = 0;

        var sut = new AppointmentController(null, context, null, null);

        var client = new Client { FirstName = "Hansie", LastName = "Bassie", Email = "testEmaghfghfghil@gmail.com", Condition = "ADHD", AgeCategory = AgeCategory.Oudste };

        //Act
        sut.Index(client, DateTime.Now.Date, DateTime.Parse(DateTime.Now.ToString("hh:mm")), null);


        //Assert
        Assert.Equal(expected, context.Appointments.Where(c => c.IncomingClientId == client.Id).Select(g => g.Guardians).Count());
    }

    [Fact]
    public void Index_AppointmentInContext_NotNull()
    {

        var context = ManagerContainer.GetWDPRContext();

        //Arrange
        var expected = 0;

        var sut = new AppointmentController(null, context, null, null);

        var client = new Client { FirstName = "Hansie", LastName = "Bassie", Email = "testEmaghfghfghil@gmail.com", Condition = "ADHD", AgeCategory = AgeCategory.Jongste };

        //Act
        sut.Index(client, DateTime.Now.Date, DateTime.Parse(DateTime.Now.ToString("hh:mm")), "Ouder@email.com");


        //Assert
        Assert.NotNull(context.Appointments);
    }

}