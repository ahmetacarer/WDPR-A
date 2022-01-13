using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WDPR_A.Models;

namespace WDPR_A.Controllers;

public class SpecialismenController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public SpecialismenController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Afdeling (string id) {
        switch(id) 
        {
            case "ADHD": 
                return View("ADHD"); 

            case "Faalangst": 
                return View("Faalangst"); 

            case "Eetstoornis": 
                return View("Eetstoornis"); 

            case "Dyslexie": 
                return View("Dyslexie"); 
        }
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
