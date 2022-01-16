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

// [Authorize(Roles = "Orthopedagogue")]
public class OrthopedagogueController : Controller
{
    private readonly ILogger<OrthopedagogueController> _logger;
    private readonly WDPRContext _context;

    public OrthopedagogueController(ILogger<OrthopedagogueController> logger, WDPRContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Dashboard()
    {
        Orthopedagogue Orthopedagogue = _context.Orthopedagogues.Where(o => o.Specialty == "ADHD").First();
        List<Appointment> appointments = _context.Appointments.Include(a => a.IncomingClient).Include(a => a.Guardians).Where(a => a.OrthopedagogueId == Orthopedagogue.Id).OrderBy(a => a.AppointmentDate).ToList();
        return View(appointments);
    }

    public async Task<IActionResult> Registration(int appointmentId)
    {
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

    public IActionResult ClientCheck()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> OnPostPartial(int BSN, DateTime birthDate)
    {
        string result = await APIcall.GetClientFile(birthDate.ToString("dd MM yyyy"), BSN);

        if (result.Equals("Error"))
        {
            return PartialView("_ClientFile", model: null);
        }
        ClientFile clientFile = JsonSerializer.Deserialize<ClientFile>(result);

        return PartialView("_ClientFile", model: clientFile);
    }

    [HttpPost]
    public async Task<IActionResult> AcceptClient(int appointmentId)
    {
        
        // hier een email verzenden om zijn wachtwoord in te stellen en vervolgens update in de database.
        var appointment = await _context.Appointments.Include(a => a.Guardians)
                                               .Include(c => c.IncomingClient)
                                               .SingleOrDefaultAsync(a => a.Id == appointmentId);
        if (appointment == null) 
            return RedirectToAction("Dashboard");
        if (appointment.Guardians != null && appointment.Guardians.Count > 0)
            await AppointmentController.SendEmail(appointment.Guardians[0].Email, "Wachtwoord Aanmaken", "");

        await AppointmentController.SendEmail(appointment.IncomingClient.Email, "Wachtwoord Aanmaken", "");
        return RedirectToAction("Dashboard");
    }

    [HttpPost]
    public async Task<IActionResult> DenyClient(int appointmentId)
    {
        var appointment = await _context.Appointments.Include(a => a.Guardians)
                                               .ThenInclude(g => g.Clients)
                                               .Include(c => c.IncomingClient)
                                               .SingleOrDefaultAsync(a => a.Id == appointmentId);
        if (appointment == null) 
            return RedirectToAction("Dashboard");
        
        var guardiansWithOneChild = _context.Guardians.Where(g => g.Clients.Count == 1 && g.Clients.Any(c => c.Id == appointment.IncomingClientId));

        if (guardiansWithOneChild != null) _context.RemoveRange(guardiansWithOneChild);
        _context.Remove(appointment.IncomingClient);
        _context.Remove(appointment);
        await _context.SaveChangesAsync();
        return RedirectToAction("Dashboard");
    }

    [HttpPost]
    public async Task<IActionResult> CreatePassword(string userId)
    {
        return RedirectToAction("index", "home");
    }
}