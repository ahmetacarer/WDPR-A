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
    
    [Fact]
    public void IndexTest () {

        DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
        WDPRContext WDPRContext = new WDPRContext(options);

        //Arrange
        var loggerMock = new Mock<ILogger<AppointmentController>>();
        //var contextMock = new Mock<WDPRContext>(); //hier gaat het mis

        var controller = new AppointmentController(loggerMock.Object, WDPRContext);

        //Act
        var result = controller.Index();

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void MakeFirstNameCapitalLetter_Test_CapitalLetter () {

        DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
        WDPRContext WDPRContext = new WDPRContext(options);

        //Arrange
        var controller = new AppointmentController(null, WDPRContext);

        var firstName = "hAns";


        //Act
        var result = controller.MakeFirstNameCapitalLetter(firstName);


        //Assert
        var viewResult = Assert.IsType<string>(result);
        Assert.Equal("Hans", result);
    }

    [Fact]
    public void Appointment_WithParent_NotNull () {

        DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
        WDPRContext WDPRContext = new WDPRContext(options);

        //Arrange
        var loggerMock = new Mock<ILogger<AppointmentController>>();
        var expected = 0;

        var sut = new AppointmentController(null, WDPRContext);

        var client = new Client {FirstName = "Hansie", LastName = "Bassie", Email = "testEmaghfghfghil@gmail.com", Condition = "ADHD"};

        //Act
        sut.Index(client, DateTime.Now, "Ouder@email.com");
        

        //Assert
        //Assert.NotNull(WDPRContext.Appointments);  //DEZEE WERKT 
        Assert.NotNull(WDPRContext.Appointments.Where(c => c.IncomingClientId == client.Id).Select(g => g.Guardians));
        


        
        
    }

    [Fact]
    public void Appointment_WithoutParent_Zero()
    {

        DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
        WDPRContext WDPRContext = new WDPRContext(options);

        //Arrange
        var loggerMock = new Mock<ILogger<AppointmentController>>();
        var expected = 0;

        var sut = new AppointmentController(null, WDPRContext);

        var client = new Client { FirstName = "Hansie", LastName = "Bassie", Email = "testEmaghfghfghil@gmail.com", Condition = "ADHD" };

        //Act
        sut.Index(client, DateTime.Now);


        //Assert
        //Assert.NotNull(WDPRContext.Appointments);  //DEZEE WERKT 
        Assert.Equal(0, WDPRContext.Appointments.Where(c => c.IncomingClientId == client.Id).Select(g => g.Guardians).Count());

    }

    [Fact]
    public void Appointment_appointmentInContext_NotNull()
    {

        DbContextOptions<WDPRContext> options = new DbContextOptionsBuilder<WDPRContext>().UseInMemoryDatabase("MijnDatabase").Options;
        WDPRContext WDPRContext = new WDPRContext(options);

        //Arrange
        var loggerMock = new Mock<ILogger<AppointmentController>>();
        var expected = 0;

        var sut = new AppointmentController(null, WDPRContext);

        var client = new Client { FirstName = "Hansie", LastName = "Bassie", Email = "testEmaghfghfghil@gmail.com", Condition = "ADHD" };

        //Act
        sut.Index(client, DateTime.Now, "Ouder@email.com");


        //Assert
        Assert.NotNull(WDPRContext.Appointments);
    }

}