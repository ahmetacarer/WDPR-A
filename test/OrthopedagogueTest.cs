// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using WDPR_A.Controllers;
// using WDPR_A.Models;
// using Xunit;

// namespace test;

// public class OrthopedagogueTest
// {
//     public WDPRContext GetWDPRContext()
//     {
//         var options = new DbContextOptionsBuilder<WDPRContext>().EnableSensitiveDataLogging().
//                         UseInMemoryDatabase("MijnDatabase")
//                         .Options;
//         var context = new WDPRContext(options);
//         return context;
//     }

//     [Fact]
//     public async Task OrthopedagogueDashboard_ViewDataTest_AppointmentsFound()
//     {
//         var context = GetWDPRContext();

//         var controller = new OrthopedagogueController(null, context, ManagerContainer.TestUserManager<IdentityUser>());

//         var orthopedagogue = new Orthopedagogue
//         {
//             Id = Guid.NewGuid().ToString(),
//             FirstName = "Jacob",
//             LastName = "Lans",
//             Specialty = "Dyslexie",
//             EmailConfirmed = true
//         };

//         var client = new Client
//         {
//             Id = Guid.NewGuid().ToString(),
//             FirstName = "Dennis",
//             LastName = "Steen",
//             Condition = "Dyslexie",
//             AgeCategory = AgeCategory.Oudste,
//             Guardians = null,
//             Chats = null,
//             Address = "Street 23",
//             Residence = "The Hague",
//             IsBlocked = false,
//             Email = "dsteen@voorbeeld.nl",
//             UserName = "dsteen@voorbeeld.nl",
//             EmailConfirmed = true
//         };

//         var appointment = new Appointment
//         {
//             Id = 1,
//             Orthopedagogue = orthopedagogue,
//             IncomingClient = client,
//         };

//         context.Orthopedagogues.Add(orthopedagogue);
//         context.Clients.Add(client);
//         context.Appointments.Add(appointment);
//         await context.SaveChangesAsync();

//         var result = await controller.Dashboard();
//         var viewResult = Assert.IsType<ViewResult>(result);

//         Assert.Null(viewResult.ViewData["Melding"]);

//         var count = Assert.IsType<List<Appointment>>(viewResult.Model).Count;
//         Assert.True(count == 1);
//     }

//     [Fact]
//     public async Task OrthopedagogueDashboard_ViewDataTest_NoAppointmentsFound()
//     {
//         var context = GetWDPRContext();

//         var controller = new OrthopedagogueController(null, context, ManagerContainer.TestUserManager<IdentityUser>());

//         var orthopedagogue = new Orthopedagogue
//         {
//             Id = Guid.NewGuid().ToString(),
//             FirstName = "Jacob",
//             LastName = "Lans",
//             Specialty = "Dyslexie",
//             EmailConfirmed = true
//         };

//         var client = new Client
//         {
//             Id = Guid.NewGuid().ToString(),
//             FirstName = "Dennis",
//             LastName = "Steen",
//             Condition = "Dyslexie",
//             AgeCategory = AgeCategory.Oudste,
//             Guardians = null,
//             Chats = null,
//             Address = "Street 23",
//             Residence = "The Hague",
//             IsBlocked = false,
//             Email = "dsteen@voorbeeld.nl",
//             UserName = "dsteen@voorbeeld.nl",
//             EmailConfirmed = false
//         };

//         context.Orthopedagogues.Add(orthopedagogue);
//         context.Clients.Add(client);
//         await context.SaveChangesAsync();

//         var result = await controller.Dashboard();
//         var viewResult = Assert.IsType<ViewResult>(result);

//         Assert.Equal("Er zijn momenteel geen afspraken ingepland.", viewResult.ViewData["Melding"].ToString());

//         var count = Assert.IsType<List<Appointment>>(viewResult.Model).Count;
//         Assert.True(count == 0);
//     }
// }