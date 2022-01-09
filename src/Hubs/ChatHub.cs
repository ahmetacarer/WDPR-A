using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using WDPR_A.Models;
// using WDPR_A.Models;

namespace WDPR_A.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WDPRContext _context;

        public ChatHub(UserManager<IdentityUser> userManager, WDPRContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task SendMessage(string user, string message)
        {
            var currentUser = await _userManager.GetUserAsync(Context.User);
            var contextUser = _context.InheritedUsers.SingleOrDefault(u => u.Id == currentUser.Id);
            var name = $"{contextUser.FirstName[0]}. {contextUser.LastName}";
            await Clients.All.SendAsync("ReceiveMessage", name, message);
        }
    }
}
