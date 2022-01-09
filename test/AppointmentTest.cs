using Xunit;
using Moq;
using System;
using WDPR_A.ViewModels;
using WDPR_A.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace test;

public class AppointmentTest
{
    
    [Fact]
    public void test () {

        //Arrange
        var loggerMock = new Mock<ILogger<AppointmentController>>();
        var contextMock = new Mock<WDPRContext>(); //hier gaat het mis

        var controller = new AppointmentController(loggerMock.Object, contextMock.Object);

        //Act
        var result = controller.Index();

        //Assert
        var viewResult = Assert.IsType<ViewResult>(result);
    }


}