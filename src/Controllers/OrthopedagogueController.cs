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
using System.Linq;
using System.Text.Encodings.Web;

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
        var currentUser = await _context.Orthopedagogues.FindAsync(user.Id);
        ViewData["Naam"] = currentUser.FirstName.FirstOrDefault() + ". " + currentUser.LastName;
        List<Appointment> appointments = await _context.Appointments.Include(a => a.IncomingClient).Include(a => a.Guardians).Include(c => c.Orthopedagogue).Where(a => a.OrthopedagogueId == currentUser.Id).OrderBy(a => a.AppointmentDate).ToListAsync();
        if (appointments.Count() == 0)
        {
            ViewData["Melding"] = "Er zijn momenteel geen afspraken ingepland.";
        }
        return View(appointments);
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
        var appointment = await _context.Appointments.Include(a => a.Guardians)
                                                     .ThenInclude(g => g.Clients)
                                               .Include(c => c.IncomingClient)
                                               .SingleOrDefaultAsync(a => a.Id == appointmentId);
        if (appointment == null || appointment.IncomingClient == null)
            return RedirectToAction("Dashboard");


        var callbackUrl = Url.Page(
            "/Account/Register",
            pageHandler: null,
            values: new { area = "Identity", userId = appointment.IncomingClientId, returnUrl = "~/" },
            protocol: Request.Scheme);
        await EmailSender.SendEmail(appointment.IncomingClient.Email, "Wachtwoord Aanmaken", $"Maak je wachtwoord aan door <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>hier</a> te klikken.");

        foreach (var guardian in appointment.Guardians.Where(g => g.PasswordHash == null && g.Clients.Count == 1))
        {
            callbackUrl = Url.Page(
                "/Account/Register",
                pageHandler: null,
                values: new { area = "Identity", userId = guardian.Email, returnUrl = "~/" },
                protocol: Request.Scheme);
            await EmailSender.SendEmail(guardian.Email, "Wachtwoord Aanmaken", $"Maak je wachtwoord aan door <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>hier</a> te klikken.");
        }
        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();
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
}