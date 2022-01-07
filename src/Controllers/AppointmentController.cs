using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WDPR_A.Models;
using System.Linq;
using System.Net.Mail;
using System.Net;
using SendGrid;
using SendGrid.Helpers.Mail;

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
    public async Task<IActionResult> Index([Bind("FirstName, LastName, Email, Condition")] Client client, [Bind("appointmentDate")] DateTime appointmentDate, [Bind("emailOfParent")] Guardian? guardian = null)
    {
        // tijdelijk
        // uitbreidbaar naar orthopedagoog met minste appointments
        if (guardian != null)
            client.Guardians = new List<Guardian>() { guardian };
        var orthopedagogue = _context.Orthopedagogues.FirstOrDefault(o => o.Specialty == client.Condition);
        Console.WriteLine(orthopedagogue == null);
        Appointment appointment = new Appointment()
        {
            AppointmentDate = appointmentDate,
            IncomingClient = client,
            Guardians = client.Guardians, // misschien null reference zonder parent
            Orthopedagogue = orthopedagogue,
            OrthopedagogueId = orthopedagogue.Id
        };
        System.Console.WriteLine(client.FirstName + client.LastName);

        _context.Appointments.Add(appointment);
        _context.SaveChanges();
        
        await Execute(client.Email);

        return RedirectToAction("Succes");
    }

    public IActionResult Succes()
    {
        return View();
    }


    static async Task Execute(string receiverEmail)
    {
        var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        Console.WriteLine(apiKey);
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("HIERKOMTEMAIL", "Example User");
        var subject = "Sending with SendGrid is Fun";
        var to = new EmailAddress(receiverEmail, "Example User");
        var plainTextContent = "and easy to do anywhere, even with C#";
        var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
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
