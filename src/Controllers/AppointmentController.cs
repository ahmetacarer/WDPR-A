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
    public async Task<IActionResult> Index([Bind("FirstName, LastName, Email, Condition")] Client client, [Bind("appointmentDate")] DateTime appointmentDate, string? emailOfParent = null)
    {
        // tijdelijk
        // uitbreidbaar naar orthopedagoog met minste appointments
        if (emailOfParent != null)
            client.Guardians = new List<Guardian>() { new Guardian { Email = emailOfParent } };
        var orthopedagogue = _context.Orthopedagogues.FirstOrDefault(o => o.Specialty == client.Condition);
        Console.WriteLine(orthopedagogue == null);
        Appointment appointment = new Appointment()
        {
            AppointmentDate = appointmentDate,
            IncomingClient = client,
            IncomingClientId = client.Id,
            Guardians = client.Guardians, // misschien null reference zonder parent
            Orthopedagogue = orthopedagogue,
            OrthopedagogueId = orthopedagogue.Id,
        };
        System.Console.WriteLine(client.FirstName + client.LastName);

        _context.Appointments.Add(appointment);
        _context.SaveChanges();
        
        // BONUS
        //
        // await Execute(client.Email, appointmentDate, false);                 

        // if (emailOfParent != null) {
        //     await Execute(emailOfParent, appointmentDate, true);
        // }

        return RedirectToAction("Succes");
    }

    public IActionResult Succes()
    {
        return View(); 
    }

    //     BONUS
    //
    // static async Task Execute(string receiverEmail, DateTime AppointmentDate, bool isParent)
    // {
    //     //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");                                //niet vergeten
    //     var apiKey = await GetApiKey();

    //     Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
    //     Console.WriteLine(apiKey);
    //     Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
    //     var client = new SendGridClient(apiKey);
    //     var from = new EmailAddress("pietsinter90@gmail.com", "ZMDH Kliniek");  //Voer verzender email in
    //     var subject = "ZMDH intakegesprek bevestiging";
    //     var to = new EmailAddress(receiverEmail, "Intakegesprek cliënt");
    //     var plainTextContent = "";
    //     string htmlContent;

    //     if (isParent) {
    //         htmlContent = "U kind heeft zich aangemeld voor een intakegesprek op <strong>" + AppointmentDate + "</strong> met email " + receiverEmail ;
    //     } else {
    //         htmlContent = "Je hebt jezelf aangemeld voor een intakegesprek op <strong>" + AppointmentDate + "</strong> met email " + receiverEmail ;
    //     }

    //     var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
    //     var response = await client.SendEmailAsync(msg);
    // }

    // public static async Task<string> GetApiKey () {
    //     var apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
    //     //await Task.Run(){return apiKey};
    //     return await Task.Run(() => { return apiKey; });
    // }


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
