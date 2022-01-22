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
    public void GuardianController_NoCurrentUser_Null()
    {
        var context = GetWDPRContext();
        var userManager = TestUserManager<IdentityUser>();
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

    // guardian can't read messages of client
    // [Fact]
    // public async Task Dashboard_GuardianWithClientUnderSixteen_FrequencyMessagesAsync()
    // {
    //     var context = GetWDPRContext();
    //     var orthopedagogue = new Orthopedagogue { Id = Guid.NewGuid().ToString(), FirstName = "", LastName = "", Specialty = "ADHD" };
    //     var client = new Client { Id = "1", FirstName = "aa", LastName = "aa", Address = "", Residence = "", Condition = "ADHD", AgeCategory = AgeCategory.Middelste, Guardians = new List<Guardian> { }, Chats = new List<Chat> { new Chat { Orthopedagogue = orthopedagogue, Messages = new HashSet<Message> { }, RoomId = Guid.NewGuid().ToString() } } };
    //     var guardian = new Guardian { Id = Guid.NewGuid().ToString(), Clients = new List<Client> { client }, FirstName = "John", LastName = "Doe", UserName = "john@doe.com" };
    //     var message = new Message { ChatRoomId = client.Chats[0].RoomId, Text = "test bericht", Sender = client };
    //     context.Add(orthopedagogue);
    //     context.Add(client);
    //     context.Add(guardian);
    //     context.Add(message);
    //     await context.SaveChangesAsync();

    //     var userManagerMock = TestUserManager<IdentityUser>();
    //     var principal = new GenericPrincipal(new ClaimsIdentity(guardian.UserName), new[] { "Guardian" });
    //     var controllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = principal }};

    //     var sut = new GuardianController(null, context, userManagerMock);
    //     sut.ControllerContext = controllerContext;
    //     var action = await sut.Dashboard();
    //     var model = Assert.IsType<ViewResult>(action).Model;
    // }

}