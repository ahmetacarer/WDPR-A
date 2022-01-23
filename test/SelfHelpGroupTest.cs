
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WDPR_A.Controllers;
using WDPR_A.Models;
using Xunit;

namespace test;

public class SelfHelpGroupTest
{



    [Fact]
    public void Create_fit_True()
    {
        var context = ManagerContainer.GetWDPRContext();
        var userManager = ManagerContainer.TestUserManager<IdentityUser>();
        var generate = ManagerContainer.GetGenerate();
        var chatManager = ManagerContainer.GetChatManager(generate, context);

        var controller = new SelfHelpGroupController(null, context, userManager, chatManager);



        var orthopedagogue = new Orthopedagogue
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Lansey",
            LastName = "Jordan",
            Specialty = "Dyslexie",
        };

        var client = new Client
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = "Dennis",
            LastName = "Steen",
            Condition = "Dyslexie",
            AgeCategory = AgeCategory.Oudste,
            Guardians = null,
            Chats = null,
            Address = "Street 23",
            Residence = "The Hague",
            IsBlocked = false,
            Email = "dsteen@voorbeeld.nl",
            UserName = "dsteen@voorbeeld.nl",
        };


    }

}