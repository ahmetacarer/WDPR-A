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

namespace test;

public class GuardianTest
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
    public void Index_RedirectToDashboard_True()
    {
        var context = GetWDPRContext();
        var sut = new GuardianController(null, context, null);
        var result = sut.Index();

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Dashboard", redirectToActionResult.ActionName);
    }

    [Fact]
    public async Task GuardianController_NoCurrentUser_True()
    {
        var context = GetWDPRContext();
        var userManager = ManagerContainer.TestUserManager<IdentityUser>();
        var sut = new GuardianController(null, context, userManager);
        Assert.Null(sut.User);
    }
}