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

    public static UserManager<TUser> TestUserManager<TUser>() where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>().Object;
        var options = new Mock<IOptions<IdentityOptions>>();
        var idOptions = new IdentityOptions();
        idOptions.Lockout.AllowedForNewUsers = false;
        options.Setup(o => o.Value).Returns(idOptions);
        var userValidators = new List<IUserValidator<TUser>>();
        var validator = new Mock<IUserValidator<TUser>>();
        userValidators.Add(validator.Object);
        var pwdValidators = new List<PasswordValidator<TUser>>();
        pwdValidators.Add(new PasswordValidator<TUser>());
        var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
            userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
            new IdentityErrorDescriber(), null,
            new Mock<ILogger<UserManager<TUser>>>().Object);
        validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
            .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
        return userManager;
    }

    public UserManager<IdentityUser> GetUserManager()
    {
        var userStoreMock = new Mock<IUserStore<IdentityUser>>();
        var optionsMock = new Mock<IOptions<IdentityOptions>>();
        var passwordHasherMock = new Mock<IPasswordHasher<IdentityUser>>();
        var userValidatorMock = new Mock<IEnumerable<IUserValidator<IdentityUser>>>();
        var passwordValidatorMock = new Mock<IEnumerable<IPasswordValidator<IdentityUser>>>();
        var lookupNormalizerMock = new Mock<ILookupNormalizer>();
        var identityErrorDescriber = new Mock<IdentityErrorDescriber>();
        var serviceProviderMock = new Mock<IServiceProvider>();
        var loggerMock = new Mock<ILogger<UserManager<IdentityUser>>>();
        var userManager = new UserManager<IdentityUser>(userStoreMock.Object, optionsMock.Object, passwordHasherMock.Object, userValidatorMock.Object,
                                                        passwordValidatorMock.Object, lookupNormalizerMock.Object, identityErrorDescriber.Object,
                                                        serviceProviderMock.Object, loggerMock.Object);
        return userManager;
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
        var userManager = TestUserManager<IdentityUser>();
        var sut = new GuardianController(null, context, userManager);
        Assert.Null(sut.User);
    }

    // [Fact]
    // public void Dashboard_Guardian_Authorize()
    // {   
    //     var loginmodel = new Login {
    //     Username = "arwinortiz",
    //     Password = "123456"
    // };
    //     var controller = new GuardianController(null, GetWDPRContext(), null);
    //     var mock = new Mock<ControllerContext>();
    //     mock.SetupGet(x => x.HttpContext.User.Identity.Name).Returns("Guardian");
    //     mock.Setup(x => x.HttpContext.User.IsInRole("Guardian")).Returns(true);
    //     controller.ControllerContext = mock.Object;

    // }
}