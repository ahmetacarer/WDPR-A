using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Models;

namespace WDPR_A.Controllers;

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
        // messages = await _context.Messages.ToListAsync();

        // if (ModelState.IsValid)
        // {


        // }

        return View();
    }

    // public async Task<IActionResult> Privacy()
    // {
    //     var currentUser = await _userManager.GetUserAsync(User);
    //     ViewBag.CurrentUserName = currentUser,
    //     var messages = await _context.Messages.ToListAsync();
    //     return View();
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
