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
            await context.AddAsync(GetDummyGuardian);
            await context.AddAsync(GetDummyClient);
            await context.SaveChangesAsync();
            return context;
        }

        public Guardian GetDummyGuardian()
        {
            return new Guardian { Clients = new List<Client> { GetDummyClient() }, FirstName = "Henkie", LastName = "Penkie"};
        }

        public Client GetDummyClient()
        {
            var client = new Client {Id = new Guid().ToString(), AgeCategory = AgeCategory.Jongste, Condition = "ADHD" ,FirstName = "John", LastName = "Penkie", Guardians = new List<Guardian> { GetDummyGuardian() }};
            client.Chats = new List<Chat> {new Chat {RoomId = "1", Subject = "ADHD", AgeCategory = AgeCategory.Jongste}};
            client.Chats[0].Messages.Add(new Message {Sender = client, Text = "Hello World", When = DateTime.Now, ChatRoomId = client.Chats[0].RoomId});
            return client;
        }
        
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