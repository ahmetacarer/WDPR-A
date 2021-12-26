using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WDPR_A.Models;

namespace WDPR_A.Controllers;

public class AfspraakController : Controller
{
    private readonly ILogger<AfspraakController> _logger;

    public AfspraakController(ILogger<AfspraakController> logger)
    {
        _logger = logger;
    }
//
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
