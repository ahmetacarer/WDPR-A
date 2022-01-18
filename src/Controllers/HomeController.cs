using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WDPR_A.Models;
using WDPR_A.ViewModels;

namespace WDPR_A.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly WDPRContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleSystem _roleSystem;
    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, RoleSystem roleSystem, WDPRContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _roleSystem = roleSystem;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        if (User.IsInRole("Guardian"))
        {
            return RedirectToAction("Dashboard", "Guardian");
        }

        if (User.IsInRole("Orthopedagogue"))
        {
            return RedirectToAction("Dashboard", "Orthopedagogue");
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
