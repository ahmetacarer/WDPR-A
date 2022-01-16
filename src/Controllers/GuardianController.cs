using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Models;

namespace WDPR_A.Controllers;

[Authorize(Roles = "Guardian")]
public class GuardianController : Controller
{
    private readonly WDPRContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public GuardianController(ILogger<HomeController> logger, WDPRContext context, UserManager<IdentityUser> userManager)
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
        var currentUser = _context.Guardians.Include(c => c.Clients).ThenInclude(client => client.Chats).ThenInclude(a => a.Messages).Where(c => c.Id == user.Id).SingleOrDefault();
        return View(currentUser);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}