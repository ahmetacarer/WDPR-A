using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Controllers;
using WDPR_A.Models;
using WDPR_A.ViewModels;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace WDPR_A.Controllers;

[Authorize(Roles = "Orthopedagogue")]
public class OrthopedagogueController : Controller
{
    private readonly ILogger<OrthopedagogueController> _logger;
    private readonly WDPRContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public OrthopedagogueController(ILogger<OrthopedagogueController> logger, WDPRContext context, UserManager<IdentityUser> userManager)
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
        var currentUser = _context.Orthopedagogues.Where(c => c.Id == user.Id).SingleOrDefault();
        List<Appointment> appointments = _context.Appointments.Include(a => a.IncomingClient).Include(a => a.Guardians).Where(a => a.OrthopedagogueId == currentUser.Id).OrderBy(a => a.AppointmentDate).ToList();
        return View(appointments);
    }

    public async Task<IActionResult> Registration(int appointmentId)
    {
        System.Console.WriteLine(appointmentId);
        var appointment = await _context.Appointments
                                        .Include(a => a.IncomingClient)
                                        .Include(a => a.Guardians)
                                        .SingleAsync(a => a.Id == appointmentId);
        if (appointment.Guardians?.Count == null || appointment.Guardians?.Count == 0)
            return RedirectToPage("/Account/Register", new { area = "Identity", email = appointment.IncomingClient.Email });
        // only one guardian can register together with client
        return RedirectToPage("/Account/Register", new { area = "Identity", email = appointment.IncomingClient.Email, guardianEmail = appointment.Guardians[0].Email });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult Client()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> OnPostPartial(int BSN, DateTime birthDate)
    {
        Console.WriteLine(birthDate.ToString("dd MM yyyy"));
        string result = await APIcall.GetClientFile(birthDate.ToString("dd MM yyyy"), BSN);

        if (result.Equals("Error"))
        {
            return PartialView("_ClientFile", model: null);
        }
        ClientFile clientFile = JsonSerializer.Deserialize<ClientFile>(result);

        return PartialView("_ClientFile", model: clientFile);
    }
}