using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Models;

namespace WDPR_A.Controllers;

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
        System.Console.WriteLine(appointmentId);
        var appointment = await _context.Appointments
                                        .Include(a => a.IncomingClient)
                                        .Include(a => a.Guardians) 
                                        .SingleAsync(a => a.Id == appointmentId);
        if (appointment.Guardians?.Count == null || appointment.Guardians?.Count == 0)
            return RedirectToPage("/Account/Register", new { area = "Identity", email = appointment.IncomingClient.Email });
        // only one guardian can register together with client
        return RedirectToPage("/Account/Register", new { area = "Identity", email = appointment.IncomingClient.Email, guardianEmail =  appointment.Guardians[0].Email});
    }

    
    public async Task CreateAccount(User user)
    {

    }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
