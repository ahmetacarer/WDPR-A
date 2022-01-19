using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Models;

namespace WDPR_A.Controllers;

[Authorize(Roles = "Moderator")]
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

    public async Task<IActionResult> Dashboard()
    {
        IdentityUser user = await _userManager.GetUserAsync(User);
        var currentUser = _context.Orthopedagogues.Find(user.Id);
        var role = await _context.Roles.SingleAsync(r => r.Name == "Moderator");
        var moderatorIds = await _context.UserRoles.Where(u => u.RoleId == role.Id).ToListAsync();
        var orthopedagogues = await _context.Orthopedagogues.Include(c => c.Chats)
                                                      .ThenInclude(c => c.Messages)
                                                      .ThenInclude(m => m.Sender)
                                                      .Where(u => u.Id != currentUser.Id && moderatorIds.Any(m => m.UserId == u.Id)).ToListAsync();
        return View(orthopedagogues);
    }

    [HttpPost]
    public async Task<IActionResult> BlockClient(string clientId)
    {
        IdentityUser user = await _userManager.GetUserAsync(User);
        var currentUser = await _context.Orthopedagogues.FindAsync(user.Id);
        var client = await _context.Clients.SingleOrDefaultAsync(c => c.Id == clientId);

        if (client == null)
            return RedirectToAction("Dashboard", "Moderator");

        client.IsBlocked = true;
        await _context.SaveChangesAsync();
        return RedirectToAction("Dashboard", "Moderator");
    }

    [HttpPost]
    public async Task<IActionResult> UnblockClient(string clientId)
    {
        IdentityUser user = await _userManager.GetUserAsync(User);
        var currentUser = await _context.Orthopedagogues.FindAsync(user.Id);
        var client = await _context.Clients.SingleOrDefaultAsync(c => c.Id == clientId);

        if (client == null)
            return RedirectToAction("Dashboard", "Moderator");

        client.IsBlocked = false;
        await _context.SaveChangesAsync();
        return RedirectToAction("Dashboard", "Moderator");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}