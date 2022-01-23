using Xunit;
using Moq;
using System;
using WDPR_A.ViewModels;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Controllers;
using Microsoft.AspNetCore.Identity;
using src.Controllers;
using System.Threading.Tasks;
using System.Collections.Generic;
using WDPR_A.Models;
using WDPR_A.Hubs;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Dynamic;
using Microsoft.AspNetCore.SignalR;

namespace test;

public class ChatTest
{

    //NameOfMethod_Scenario_Expected
    public async Task<WDPRContext> GetWDPRContextAsync()
    {
        var options = new DbContextOptionsBuilder<WDPRContext>().EnableSensitiveDataLogging().
                        UseInMemoryDatabase("MijnDatabase")
                        .Options;
        var context = new WDPRContext(options);
        //await context.AddAsync(GetDummyGuardian);
        // await context.AddAsync(GetDummyClient);
        // await context.SaveChangesAsync();
        return context;
    }

    public Guardian GetDummyGuardian()
    {
        return new Guardian { Clients = new List<Client> { GetDummyClient() }, FirstName = "Henkie", LastName = "Penkie" };
    }

    public Client GetDummyClient()
    {
        var client = new Client { Id = new Guid().ToString(), AgeCategory = AgeCategory.Jongste, Condition = "ADHD", FirstName = "John", LastName = "Penkie" };
        client.Chats = new List<Chat> { new Chat { RoomId = "1", Subject = "ADHD", AgeCategory = AgeCategory.Oudste } };
        client.Chats[0].Messages.Add(new Message { Sender = client, Text = "Hello World", When = DateTime.Now, ChatRoomId = client.Chats[0].RoomId });
        return client;
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

    //5

    // [Fact]
    // public async Task HubsAreMockableViaDynamic()
    // {
    //     bool sendCalled = false;
    //     var hub = new ChatHub(TestUserManager<IdentityUser>(), await GetWDPRContextAsync());
    //     var mockClients = new Mock<IHubCallerClients>();
    //     hub.Clients = mockClients.Object;
    //     dynamic all = new ExpandoObject();
    //     all.broadcastMessage = new Action<string, string>((name, message) =>
    //     {
    //         sendCalled = true;
    //     });
    //     mockClients.Setup(m => m.All).Returns(all);
    //     await hub.SendMessage("TestUser", "1");
    //     Assert.True(sendCalled);
    // }

    // [Fact]
    // public async Task SendNotification()
    // {
    //     Mock<IHubClients> mockClients = new Mock<IHubClients>();
    //     Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();
    //     mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);

    //     var hubContext = new Mock<IHubContext<ChatHub>>();
    //     hubContext.Setup(x => x.Clients).Returns(() => mockClients.Object);

    //     //var db = MyDBMock.GetMock();
    //     ChatHub hub = new ChatHub(hubContext.Object, db);

    //     await hub.Clients.All("Yo! This is the unit test.");

    //     mockClients.Verify(clients => clients.All, Times.Once);
    // }

    // [Fact]
    // public async Task SignalR_OnConnect_ShouldReturn3Messages()

    // {
    //     // arrange
    //     Mock<IHubCallerClients> mockClients = new Mock<IHubCallerClients>();
    //     Mock<IClientProxy> mockClientProxy = new Mock<IClientProxy>();

    //     mockClients.Setup(clients => clients.All).Returns(mockClientProxy.Object);


    //     ChatHub simpleHub = new ChatHub()
    //     {
    //         Clients = mockClients.Object
    //     };

    //     // act
    //     mockClients.simpleHub.Clients.All;


    //     // assert
    //     mockClients.Verify(clients => clients.All, Times.Once);

    //     mockClientProxy.Verify(
    //         clientProxy => clientProxy.SendCoreAsync(
    //             "welcome",
    //             It.Is<object[]>(o => o != null && o.Length == 1 && ((object[])o[0]).Length == 3),
    //             default(CancellationToken)),
    //         Times.Once);
    // }


    // [Fact]
    // public async Task TestAsync() 
    // {
    //     var context = await GetWDPRContextAsync();
    //     var userManagerMock = new Mock<UserManager<IdentityUser>>();

    //     var controller = new ChatController(null, context, userManagerMock.Object);
    //     var sut = controller.Index();
    // }

    // requirement 6 testen
    //Guardian aanmakenm
    //controller anmaken
    //guardian de controllermethod Index laten oproepen
    //Assert verwacht een Access Denied View
}