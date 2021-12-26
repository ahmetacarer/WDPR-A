using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WDPR_A.Models;

namespace WDPR_A.Controllers;

// [Authorize(Roles = "Guardian")]
public class GuardianController : Controller
{
    private readonly WDPRContext _context;
    private readonly ILogger<HomeController> _logger;

    public GuardianController(ILogger<HomeController> logger, WDPRContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Dashboard()
    {
        Guardian guardian = new Guardian()
        {
            FirstName = "Willem",
            LastName = "Bommel",
            Clients = new List<Client> {
        new Client() { FirstName = "Erik", LastName = "Bommel", Condition = "ADHD" },
        new Client() { FirstName = "Jordy", LastName = "Bommel", Condition = "Autisme" }
        }
        };
        return View(guardian);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
