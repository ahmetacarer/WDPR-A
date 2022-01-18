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

        if (_context.Users.Any(b => b.Email == client.Email))
        {

            return RedirectToAction("Index");
        }

        // tijdelijk
        // uitbreidbaar naar orthopedagoog met minste appointments
        client.FirstName = MakeFirstNameCapitalLetter(client.FirstName);
        client.LastName = client.LastName.ToLower();

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

        SendEmailThroughMailhog(client.Email, appointmentDate, false);

        if (emailOfParent != null)
        {
            SendEmailThroughMailhog(emailOfParent, appointmentDate, true);
        }

        System.Console.WriteLine(client.FirstName + " " + client.LastName);

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

    public void SendEmailThroughMailhog(string receiverEmail, DateTime AppointmentDate, bool isParent)
    {
        //AppointmentDate.ToString("MM/dd/yyyy hh:mm");
        string datum = AppointmentDate.ToString("MM/dd/yyyy");
        Console.WriteLine(datum);
        string tijd = AppointmentDate.ToString("HH:mm");
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine(tijd);

        //execute powershell cmdlets or scripts using command arguments as process
        ProcessStartInfo processInfo = new ProcessStartInfo();
        processInfo.FileName = @"powershell.exe";
        //execute powershell script using script file
        //processInfo.Arguments = @"& {c:\temp\Get-EventLog.ps1}";
        //execute powershell command

        if (isParent)
        {
            processInfo.Arguments = $@"& Send-MailMessage -To '{receiverEmail}' -From 'no-reply@ZMDHKliniek.com' -Condition 'ZMDH intakegesprek bevestiging' -Body 'U kind heeft zich aangemeld voor een intakegesprek op {datum} om {tijd}' -SmtpServer 'localhost' -Port 1025";

        }
        else
        {
            processInfo.Arguments = $@"& Send-MailMessage -To '{receiverEmail}' -From 'no-reply@ZMDHKliniek.com' -Condition 'ZMDH intakegesprek bevestiging' -Body 'Je hebt jezelf aangemeld voor een intakegesprek op {datum} om {tijd}' -SmtpServer 'localhost' -Port 1025";

        }

        //processInfo.Arguments = $@"& Send-MailMessage -To '{receiverEmail}' -From 'no-reply@ZMDHKliniek.com' -Condition 'ZMDH intakegesprek bevestiging' -Body 'Je hebt jezelf aangemeld voor een intakegesprek op {AppointmentDate} met email {receiverEmail}' -SmtpServer 'localhost' -Port 1025";
        processInfo.RedirectStandardError = true;
        processInfo.RedirectStandardOutput = true;
        processInfo.UseShellExecute = false;
        processInfo.CreateNoWindow = true;

        //start powershell process using process start info
        Process process = new Process();
        process.StartInfo = processInfo;
        process.Start();

        Console.WriteLine("Output - {0}", process.StandardOutput.ReadToEnd());
        Console.WriteLine("Errors - {0}", process.StandardError.ReadToEnd());
        process.Close();
    }

    public string MakeFirstNameCapitalLetter(string firstName)
    {
        if (firstName.Length > 1)
        { //Execute the following code when the name is longer than 1 character
            string capitalLetter = firstName.Substring(0, 1);
            capitalLetter = capitalLetter.ToUpper();
            string restaint = firstName.Substring(1);
            restaint = restaint.ToLower();
            string good = capitalLetter + restaint;

            return good;
        }
        else
        {
            return firstName.ToUpper();
        }
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
    //     var from = new EmailAddress("[HIER KOMT SENDGRID EMAIL]]", "ZMDH Kliniek");  //Voer verzender email in
    //     var condition = "ZMDH intakegesprek bevestiging";
    //     var to = new EmailAddress(receiverEmail, "Intakegesprek cliënt");
    //     var plainTextContent = "";
    //     string htmlContent;

    //     if (isParent) {
    //         htmlContent = "U kind heeft zich aangemeld voor een intakegesprek op <strong>" + AppointmentDate + "</strong> met email " + receiverEmail ;
    //     } else {
    //         htmlContent = "Je hebt jezelf aangemeld voor een intakegesprek op <strong>" + AppointmentDate + "</strong> met email " + receiverEmail ;
    //     }

    //     var msg = MailHelper.CreateSingleEmail(from, to, condition, plainTextContent, htmlContent);
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
