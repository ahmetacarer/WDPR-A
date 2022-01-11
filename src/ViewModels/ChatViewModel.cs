using Microsoft.AspNetCore.Identity;
using WDPR_A.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WDPR_A.ViewModels;

public class ChatViewModel
{
    private readonly WDPRContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly HttpContext _httpContext;

    public ChatViewModel(WDPRContext context, UserManager<IdentityUser> userManager, HttpContextAccessor accessor)
    {
        _context = context;
        _userManager = userManager;
        _httpContext = accessor.HttpContext;
    }

    public async Task<List<Chat>> GetChatsAsync()
    {
        IdentityUser user = await _userManager.GetUserAsync(_httpContext.User);
        return await _context.Chats.Where(c => c.Orthopedagogue.Id == user.Id || c.Clients.Any(cl => cl.Id == user.Id)).ToListAsync();
    }
}