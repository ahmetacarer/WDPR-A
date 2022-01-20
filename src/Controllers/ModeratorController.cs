using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Models;

namespace WDPR_A.Controllers;

// [Authorize(Roles = "Moderator")]
public class ModeratorController : Controller
{
    private readonly WDPRContext _context;
    private readonly ILogger<ModeratorController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public ModeratorController(ILogger<ModeratorController> logger, WDPRContext context, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Dashboard");
    }
    public async Task<IActionResult> Dashboard(string? waarde)
    {
        // IdentityUser user = await _userManager.GetUserAsync(User);
        // var currentUser = _context.Orthopedagogues.Find(user.Id);
        // var role = await _context.Roles.SingleAsync(r => r.Name == "Moderator");
        // var moderatorIds = await _context.UserRoles.Where(u => u.RoleId == role.Id).ToListAsync();
        // var orthopedagogues = await _context.Orthopedagogues.Include(c => c.Chats)
        //                                               .ThenInclude(c => c.Messages)
        //                                               .ThenInclude(m => m.Sender)
        //                                               .Where(u => u.Id != currentUser.Id).ToListAsync();

        // if (waarde == "blockedClients")
        // {
        //     var list = await showAllBlockClients();
        //     return View(list);
        // }

        // else if (waarde == "reportedMessages")
        // {
        //     var list = await showAllReportedMessages();
        //     return View(list);
        // }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> showAllBlockClients()
    {
        var clients = await _context.Clients.Where(c => c.IsBlocked).ToListAsync();
        return PartialView("_blockedClients", clients);
    }

    [HttpPost]
    public async Task<IActionResult> showAllReportedMessages()
    {
        var reportedMessages = await _context.Messages.Include(c => c.Sender)
                                            .Include(c => c.Chat)
                                            .ThenInclude(c => c.Clients)
                                            .Where(c => c.ReportCount > 0 && c.Chat.Clients.Any(cl => cl.Id == c.Sender.Id && (cl.IsBlocked == null || cl.IsBlocked == false)))
                                            .ToListAsync();
        return PartialView("_reportedMessages", reportedMessages);
    }

    [HttpPost]
    public async Task<IActionResult> BlockClient(string clientId)
    {
        var client = await _context.Clients.SingleOrDefaultAsync(c => c.Id == clientId);

        // var test = await _context.Messages.Include(c => c.Chat)
        //                                 .Include(c => c.Sender)
        //                                 .Where(c => c.ReportCount > 0 && c.Chat.Clients.Where(c => c.IsBlocked == false))
        //                                 .ToListAsync();

        if (client == null)
            return RedirectToAction("Dashboard", "Moderator");

        client.IsBlocked = true;
        await _context.SaveChangesAsync();
        return RedirectToAction("showAllReportedMessages");
    }

    [HttpPost]
    public async Task<IActionResult> UnblockClient(string clientId)
    {
        var client = await _context.Clients.SingleOrDefaultAsync(c => c.Id == clientId);

        if (client == null)
            return RedirectToAction("Dashboard", "Moderator");

        client.IsBlocked = false;
        await _context.SaveChangesAsync();
        return RedirectToAction();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}