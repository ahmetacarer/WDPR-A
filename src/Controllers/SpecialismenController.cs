using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WDPR_A.Models;

namespace WDPR_A.Controllers;

public class SpecialismenController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly WDPRContext _context;

    public SpecialismenController(ILogger<HomeController> logger, WDPRContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Afdeling (string id) {
        switch(id) 
        {
            case "ADHD": 
                return View("ADHD", _context.Orthopedagogues.Where(g => g.Specialty == "ADHD").ToList()); 

            case "Faalangst": 
                return View("Faalangst", _context.Orthopedagogues.Where(g => g.Specialty == "Faalangst").ToList()); 

            case "Eetstoornis": 
                return View("Eetstoornis", _context.Orthopedagogues.Where(g => g.Specialty == "Eetstoornis").ToList()); 

            case "Dyslexie": 
                return View("Dyslexie", _context.Orthopedagogues.Where(g => g.Specialty == "Dyslexie").ToList()); 
        }
        return RedirectToAction("Index");
    }

    public IActionResult Specialist (string id) {
        return View(_context.Orthopedagogues.Where(g => g.Id == id).ToList());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
