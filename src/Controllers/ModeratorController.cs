using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WDPR_A.Models;

namespace WDPR_A.Controllers;

[Authorize(Roles = "Moderator")]
public class ModeratorController : Controller
{
    private readonly WDPRContext _context;
    private readonly ILogger<ModeratorController> _logger;
    private readonly UserManager<IdentityUser> _userManager;

    public ModeratorController(ILogger<ModeratorController> logger, WDPRContext context, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return RedirectToAction("Dashboard");
    }
    public IActionResult Panel()
    {
        return View();
    }

    public async Task<IActionResult> Dashboard(string searchString)
    {
        var list = _context.Appointments.Include(c => c.IncomingClient).Include(c => c.Orthopedagogue).Where(c => c.IsVerified);

        if (!String.IsNullOrEmpty(searchString))
        {
            list = list.Where(c => c.IncomingClient.FirstName.ToLower().Substring(0, searchString.Length) == searchString.ToLower());
        }

        if (list.Count() == 0)
        {
            ViewData["Melding"] = "Er zijn geen behandelingen gevonden.";
        }

        return View(await list.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> showAllBlockClients(string clientId)
    {
        if (!String.IsNullOrEmpty(clientId))
        {
            var result = UnblockClient(clientId);
        }

        var clients = await _context.Clients.Where(c => c.IsBlocked).ToListAsync();
        return PartialView("_blockedClients", clients);
    }

    [HttpPost]
    public async Task<IActionResult> showAllReportedMessages(string clientId, string messageId)
    {
        if (!String.IsNullOrEmpty(clientId))
        {
            var result = BlockClient(clientId);
        }

        if (!String.IsNullOrEmpty(messageId))
        {
            var result = IgnoreMessage(messageId);
        }

        var reportedMessages = await _context.Messages.Include(c => c.Sender)
                                            .Include(c => c.Chat)
                                            .ThenInclude(c => c.Clients)
                                            .Where(c => c.ReportCount > 0 && c.Chat.Clients.Any(cl => cl.Id == c.Sender.Id && (cl.IsBlocked == null || cl.IsBlocked == false)))
                                            .ToListAsync();
        return PartialView("_reportedMessages", reportedMessages);
    }

    public async Task<Boolean> EmailPosting(Client client)
    {
        var orthopedagogue = _context.Orthopedagogues.FirstOrDefault(o => o.Specialty == client.Condition);
        await EmailSender.SendEmail(orthopedagogue.Email, "Uw client vertoont grof gedrag en is daarom geblokkeerd",
        $"Uw client: <strong>{client.FirstName} {client.LastName}</strong> had grof gedrag en verkeerd taalgebruik vertoont in de chat en is bij deze geblokkeerd op {DateTime.Now}");

        return true;
    }
    [HttpPost]
    public async Task<Boolean> BlockClient(string clientId)
    {
        var client = await _context.Clients.SingleOrDefaultAsync(c => c.Id == clientId);
        var messages = await _context.Messages.Where(m => client.Id == m.Sender.Id && m.ReportCount > 0).ToListAsync();

        client.IsBlocked = true;
        _context.Messages.RemoveRange(messages);
        await _context.SaveChangesAsync();
        // var input = await EmailPosting(client);

        return true;
    }

    public async Task<Boolean> UnblockClient(string clientId)
    {
        var client = await _context.Clients.SingleOrDefaultAsync(c => c.Id == clientId);

        client.IsBlocked = false;
        _context.Messages.Where(c => c.Sender.Id == client.Id).ToList().ForEach(c => c.ReportCount = 0);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<Boolean> IgnoreMessage(string messageId)
    {
        var message = await _context.Messages.SingleOrDefaultAsync(c => c.Id == Int32.Parse(messageId));
        message.ReportCount = 0;
        await _context.SaveChangesAsync();

        return true;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}