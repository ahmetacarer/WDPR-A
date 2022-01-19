using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Extensions;
using WDPR_A.Models;

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
        IdentityUser user = await _userManager.GetUserAsync(User);
        var chats = new List<Chat>();

        if (User.IsInRole("Client"))
        {
            var currentUser = await _context.Clients.FindAsync(user.Id);
            var client = _context.Clients.FirstOrDefault(c => c.Id == currentUser.Id);
            chats = await _context.Chats.Include(c => c.Orthopedagogue)
                                            .Include(c => c.Clients)
                                            .Include(c => c.Messages)
                                            .Where(c => c.Clients.Any(cl => cl.Id == client.Id))
                                            .ToListAsync();
        }

        else
        {
            var currentUser = await _context.Orthopedagogues.FindAsync(user.Id);
            var orthopedagogue = _context.Orthopedagogues.FirstOrDefault(c => c.Id == currentUser.Id);
            chats = await _context.Chats.Include(c => c.Clients).Include(c => c.Messages).Where(c => c.Orthopedagogue.Id == orthopedagogue.Id).ToListAsync();
        }

        return View(chats);
    }
    [HttpPost]
    public async Task ReportClient(int messageId)
    {
        var reportedMessage = await _context.Messages.FindAsync(messageId);
        if (reportedMessage != null)
        {
            reportedMessage.ReportCount++;
            await _context.SaveChangesAsync();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpPost]
    public async Task<IActionResult> OnGetChatPartial(string chatRoomId)
    {
        bool isAjax = HttpContext.Request.IsAjax("POST");

        if (!isAjax)
            return RedirectToAction("Index", "Home");
        var chat = _context.Chats.Include(c => c.Messages)
                                 .Include(c => c.Clients)
                                 .Include(c => c.Orthopedagogue)
                                 .Where(c => c.RoomId == chatRoomId)
                                 .SingleOrDefault();

        ViewData["CurrentUserID"] =(await _userManager.GetUserAsync(User)).Id;
        return PartialView("_ChatPartial", chat);
    }
}
