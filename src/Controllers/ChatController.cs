using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Models;
using WDPR_A.Extensions;

namespace WDPR_A.Controllers;

[Authorize(Roles = "Client, Orthopedagogue")]
public class ChatController : Controller
{
    private readonly ILogger<ChatController> _logger;
    private readonly WDPRContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public ChatController(ILogger<ChatController> logger, WDPRContext context, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var currentUser = await _userManager.GetUserAsync(User);
        var client = _context.Clients.FirstOrDefault(c => c.Id == currentUser.Id);
        var chats = await _context.Chats.Include(c => c.Orthopedagogue)
                                        .Include(c => c.Clients)
                                        .Include(c => c.Messages)
                                        .Where(c => c.Clients.Any(cl => cl.Id == client.Id))
                                        .ToListAsync();
        // chats = new List<Chat> {new Chat {RoomId = "1", RoomName = "ADHDZelfhelpGroepJong", Orthopedagogue = _context.Orthopedagogues.First(), Subject = "ADHD", Clients = new List<Client> {client}, IsPrivate = false, AgeCategory = AgeCategory.Jongste},
        //                             new Chat {RoomId = "2", RoomName = "FaalangstZelfhelpGroepMiddelste", Orthopedagogue = _context.Orthopedagogues.First(), Subject = "Faalangst", Clients = new List<Client> {client}, IsPrivate = false, AgeCategory = AgeCategory.Middelste},
        //                             new Chat {RoomId = "3", PrivateChatToken = "3", Orthopedagogue = _context.Orthopedagogues.First(c => c.Specialty == "Faalangst"), Clients = new List<Client> {client}, IsPrivate = true}

        //                             };
        // await _context.Chats.AddRangeAsync(chats);
        // await _context.SaveChangesAsync();
        return View(chats);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    // nog niet async
    [HttpPost]
    public IActionResult OnGetChatPartial(string chatRoomId)
    {
        bool isAjax = HttpContext.Request.IsAjax("POST");
        
        if (!isAjax) 
            return RedirectToAction("Index", "Home");
        System.Console.WriteLine(chatRoomId);
        var chat = _context.Chats.Include(c => c.Messages)
                                 .Include(c => c.Clients)
                                 .Include(c => c.Orthopedagogue)
                                 .Where(c => c.RoomId == chatRoomId)
                                 .SingleOrDefault();
        return PartialView("_ChatPartial", chat);
    }
}
