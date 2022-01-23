using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WDPR_A.ViewModels;

namespace test;

public class ManagerContainer
{
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
    
    public static WDPRContext GetWDPRContext()
    {
        var options = new DbContextOptionsBuilder<WDPRContext>().EnableSensitiveDataLogging().
                        UseInMemoryDatabase("MijnDatabase")
                        .Options;
        var context = new WDPRContext(options);
        return context;
    }


    public static ChatManager GetChatManager (Generate generate, WDPRContext context) {

        var chatManager = new ChatManager(generate, context);
        return chatManager;
    }

    public static Generate GetGenerate () {
        var random = new Random();
        var generate = new Generate(random);
        return generate;
    }

    // public static RoleSystem GetRoleSystem (WDPRContext context) {
    //     var roleManager = new RoleManager<>();
    //     var roleSystem = new RoleSystem(context);

    //     return roleSystem;
    // }

    // public UserManager<IdentityUser> GetUserManager()
    // {
    //     var userStoreMock = new Mock<IUserStore<IdentityUser>>();
    //     var optionsMock = new Mock<IOptions<IdentityOptions>>();
    //     var passwordHasherMock = new Mock<IPasswordHasher<IdentityUser>>();
    //     var userValidatorMock = new Mock<IEnumerable<IUserValidator<IdentityUser>>>();
    //     var passwordValidatorMock = new Mock<IEnumerable<IPasswordValidator<IdentityUser>>>();
    //     var lookupNormalizerMock = new Mock<ILookupNormalizer>();
    //     var identityErrorDescriber = new Mock<IdentityErrorDescriber>();
    //     var serviceProviderMock = new Mock<IServiceProvider>();
    //     var loggerMock = new Mock<ILogger<UserManager<IdentityUser>>>();
    //     var userManager = new UserManager<IdentityUser>(userStoreMock.Object, optionsMock.Object, passwordHasherMock.Object, userValidatorMock.Object,
    //                                                     passwordValidatorMock.Object, lookupNormalizerMock.Object, identityErrorDescriber.Object,
    //                                                     serviceProviderMock.Object, loggerMock.Object);
    //     return userManager;
    // }



}