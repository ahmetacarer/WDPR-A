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

    // [Fact]
    // public void Index_ViewResultTest_True () {

    //     DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
    //     WDPRContext WDPRContext = new WDPRContext(options);

    //     //Arrange
    //     var controller = new AppointmentController(null,WDPRContext, null, null);

    //     //Act
    //     var result = controller.Index();

    //     //Assert
    //     var viewResult = Assert.IsType<ViewResult>(result);
    // }

    // [Fact]
    // public void MakeFirstNameCapitalLetter_MakeCapitalLetter_True () {

    //     DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
    //     WDPRContext WDPRContext = new WDPRContext(options);

    //     //Arrange
    //     var controller = new AppointmentController(null,WDPRContext, null, null);

    //     var firstName = "hAns";

    //     //Act
    //     var result = controller.MakeFirstNameCapitalLetter(firstName);


    //     //Assert
    //     var viewResult = Assert.IsType<string>(result);
    //     Assert.Equal("Hans", result);
    // }

    // [Fact]
    // public void Index_WithParent_NotNull () {

    //     DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
    //     WDPRContext WDPRContext = new WDPRContext(options);

    //     //Arrange
    //     var sut = new AppointmentController(null,WDPRContext, null, null);

    //     var client = new Client {FirstName = "Hansie", LastName = "Bassie", Email = "testEmail@gmail.com", Condition = "ADHD"};

    //     //Act
    //     sut.Index(client, DateTime.Now.Date, DateTime.Parse(DateTime.Now.ToString("hh:mm")), AgeCategory.Jongste, "Ouder@email.com");
        

    //     //Assert
    //     Assert.NotNull(WDPRContext.Appointments.Where(c => c.IncomingClientId == client.Id).Select(g => g.Guardians));
        


        
        
    // }

    // [Fact]
    // public void Index_WithoutParent_Zero()
    // {

    //     DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
    //     WDPRContext WDPRContext = new WDPRContext(options);

    //     //Arrange
    //     var expected = 0;

    //     var sut = new AppointmentController(null,WDPRContext, null, null);

    //     var client = new Client { FirstName = "Hansie", LastName = "Bassie", Email = "testEmaghfghfghil@gmail.com", Condition = "ADHD" };

    //     //Act
    //     sut.Index(client, DateTime.Now.Date, DateTime.Parse(DateTime.Now.ToString("hh:mm")), AgeCategory.Jongste, null);


    //     //Assert
    //     Assert.Equal(expected, WDPRContext.Appointments.Where(c => c.IncomingClientId == client.Id).Select(g => g.Guardians).Count());

    // }

    // [Fact]
    // public void Index_AppointmentInContext_NotNull()
    // {

    //     DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
    //     WDPRContext WDPRContext = new WDPRContext(options);

    //     //Arrange
    //     var expected = 0;

    //     var sut = new AppointmentController(null,WDPRContext, null, null);

    //     var client = new Client { FirstName = "Hansie", LastName = "Bassie", Email = "testEmaghfghfghil@gmail.com", Condition = "ADHD" };

    //     //Act
    //     sut.Index(client, DateTime.Now.Date, DateTime.Parse(DateTime.Now.ToString("hh:mm")), AgeCategory.Jongste, "Ouder@email.com");


    //     //Assert
    //     Assert.NotNull(WDPRContext.Appointments);
    // }

}