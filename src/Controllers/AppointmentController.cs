using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WDPR_A.Models;
using System.Linq;

namespace WDPR_A.Controllers;

public class AppointmentController : Controller
{
    private readonly WDPRContext _context;
    private readonly ILogger<AppointmentController> _logger;

    public AppointmentController(ILogger<AppointmentController> logger, WDPRContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Index([Bind("FirstName, LastName, Email")] Client client)
    {
        // tijdelijk
        // uitbreidbaar naar orthopedagoog met minste appointments

        var orthopedagogue = _context.Orthopedagogues.FirstOrDefault(o => o.Specialty == client.Condition);
        Appointment appointment = new Appointment()
        {
            AppointmentDate = DateTime.Now,
            IncomingClient = client,
            Guardians = client.Guardians,
            Orthopedagogue = orthopedagogue,
            OrthopedagogueId = orthopedagogue.Id
        };
        System.Console.WriteLine(client.FirstName + client.LastName);

        _context.Appointments.Add(appointment);
        _context.SaveChanges();

        return RedirectToAction("Succes");
    }

    public IActionResult Succes()
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
