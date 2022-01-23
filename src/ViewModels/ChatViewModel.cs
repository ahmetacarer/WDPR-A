using Microsoft.AspNetCore.Identity;
using WDPR_A.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WDPR_A.ViewModels;

public class ChatViewModel
{
    private readonly WDPRContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ClaimsPrincipal _userPrincipal;

    public ChatViewModel(WDPRContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor accessor)
    {
        _context = context;
        _userManager = userManager;
        _userPrincipal = accessor.HttpContext.User;
    }

    public async Task<List<Chat>> GetChatsAsync()
    {
        IdentityUser user = await _userManager.GetUserAsync(_userPrincipal);
        return await _context.Chats.Where(c => c.Orthopedagogue.Id == user.Id || c.Clients.Any(cl => cl.Id == user.Id)).ToListAsync();
    }
}