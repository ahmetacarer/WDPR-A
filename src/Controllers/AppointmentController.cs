using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WDPR_A.Models;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WDPR_A.Controllers;

public class AppointmentController : Controller
{
    private readonly WDPRContext _context;
    private readonly ILogger<AppointmentController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserStore<IdentityUser> _userStore;

    public AppointmentController(ILogger<AppointmentController> logger, WDPRContext context, UserManager<IdentityUser> userManager, IUserStore<IdentityUser> userStore)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _userStore = userStore;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Index([Bind("FirstName, LastName, Email, Condition, Address, Residence")] Client client, DateTime appointmentDate, DateTime appointmentTime, AgeCategory AgeCategory, string? emailOfParent = null)
    {

        if (_context.Users.Any(b => b.Email == client.Email))
        {
            ViewData["foutmelding"] = "email bestaat al";
            return View();
        }

        // tijdelijk
        // uitbreidbaar naar orthopedagoog met minste appointments
        client.FirstName = MakeFirstNameCapitalLetter(client.FirstName);
        client.LastName = client.LastName.ToLower();

        if (emailOfParent != null)
        {

            client.Guardians = new List<Guardian>() { new Guardian { Email = emailOfParent } };
            await _userStore.SetUserNameAsync(client.Guardians[0], emailOfParent, CancellationToken.None);
        }

        var orthopedagogue = _context.Orthopedagogues.FirstOrDefault(o => o.Specialty == client.Condition);
        Appointment appointment = new Appointment()
        {
            AppointmentDate = appointmentDate.Date + new TimeSpan(appointmentTime.Hour, appointmentTime.Minute, 0),
            IncomingClient = client,
            IncomingClientId = client.Id,
            Guardians = client.Guardians, // misschien null reference zonder parent
            Orthopedagogue = orthopedagogue,
            OrthopedagogueId = orthopedagogue.Id,
        };

        // SendEmailThroughMailhog(client.Email, appointmentDate, false);

        // if (emailOfParent != null)
        // {
        //     SendEmailThroughMailhog(emailOfParent, appointmentDate, true);
        // }

        await _userStore.SetUserNameAsync(client, client.Email, CancellationToken.None);
        _context.Appointments.Add(appointment);
        _context.SaveChanges();

        var clientToken = await _userManager.GenerateEmailConfirmationTokenAsync(client);
        clientToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(clientToken));
        var callBackUrlClient = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId = client.Id, code = clientToken, returnUrl = "~/" },
            protocol: Request.Scheme);

        string datum = appointment.AppointmentDate.ToString("dd/MM/yyyy HH:mm");
        await EmailSender.SendEmail(client.Email, "Bevestig je mail",
            $"Bevestig je mail door te <a href='{HtmlEncoder.Default.Encode(callBackUrlClient)}'>klikken</a>.</br>Jouw intake-gesprek vindt plaats op {datum}");

        if (emailOfParent != null)
        {
            var guardianToken = await _userManager.GenerateEmailConfirmationTokenAsync(client.Guardians[0]);
            guardianToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(guardianToken));
            var callBackUrlGuardian = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId = client.Guardians[0].Id, code = guardianToken, returnUrl = "~/" },
            protocol: Request.Scheme);
            await EmailSender.SendEmail(emailOfParent, "Bevestig je mail",
                $"Bevestig je mail door te <a href='{HtmlEncoder.Default.Encode(callBackUrlGuardian)}'>klikken</a>.</br>Jouw intake-gesprek vindt plaats op {datum}");
        }
        return RedirectToAction("SuccessAppointment");
    }

    public IActionResult SuccessAppointment()
    {
        return View();
    }


    // public void SendEmailThroughMailhog(string receiverEmail, DateTime AppointmentDate, bool isParent)
    // {
    //     //AppointmentDate.ToString("MM/dd/yyyy hh:mm");
    //     string datum = AppointmentDate.ToString("MM/dd/yyyy");
    //     string tijd = AppointmentDate.ToString("HH:mm");

    //     //execute powershell cmdlets or scripts using command arguments as process
    //     ProcessStartInfo processInfo = new ProcessStartInfo();
    //     processInfo.FileName = @"powershell.exe";
    //     //execute powershell script using script file
    //     //processInfo.Arguments = @"& {c:\temp\Get-EventLog.ps1}";
    //     //execute powershell command

    //     if (isParent)
    //     {
    //         processInfo.Arguments = $@"& Send-MailMessage -To '{receiverEmail}' -From 'no-reply@ZMDHKliniek.com' -Subject 'ZMDH intakegesprek bevestiging' -Body 'U kind heeft zich aangemeld voor een intakegesprek op {datum} om {tijd}' -SmtpServer 'localhost' -Port 1025";
    //     }

    //     //execute powershell cmdlets or scripts using command arguments as process
    //     ProcessStartInfo processInfo = new ProcessStartInfo();
    //     processInfo.FileName = @"powershell.exe";
    //     //execute powershell script using script file
    //     //processInfo.Arguments = @"& {c:\temp\Get-EventLog.ps1}";
    //     //execute powershell command

    //     if (isParent)
    //     {
    //         processInfo.Arguments = $@"& Send-MailMessage -To '{receiverEmail}' -From 'no-reply@ZMDHKliniek.com' -Subject 'ZMDH intakegesprek bevestiging' -Body 'U kind heeft zich aangemeld voor een intakegesprek op {datum} om {tijd}' -SmtpServer 'localhost' -Port 1025";

    //     }
    //     else
    //     {
    //         processInfo.Arguments = $@"& Send-MailMessage -To '{receiverEmail}' -From 'no-reply@ZMDHKliniek.com' -Subject 'ZMDH intakegesprek bevestiging' -Body 'Je hebt jezelf aangemeld voor een intakegesprek op {datum} om {tijd}' -SmtpServer 'localhost' -Port 1025";

    //     }

    //     //processInfo.Arguments = $@"& Send-MailMessage -To '{receiverEmail}' -From 'no-reply@ZMDHKliniek.com' -Subject 'ZMDH intakegesprek bevestiging' -Body 'Je hebt jezelf aangemeld voor een intakegesprek op {AppointmentDate} met email {receiverEmail}' -SmtpServer 'localhost' -Port 1025";
    //     processInfo.RedirectStandardError = true;
    //     processInfo.RedirectStandardOutput = true;
    //     processInfo.UseShellExecute = false;
    //     processInfo.CreateNoWindow = true;

    //     //start powershell process using process start info
    //     Process process = new Process();
    //     process.StartInfo = processInfo;
    //     process.Start();
    //     process.Close();
    // }

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
