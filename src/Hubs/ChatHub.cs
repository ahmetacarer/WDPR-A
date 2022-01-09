using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
// using WDPR_A.Models;

namespace WDPR_A.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ChatHub(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SendMessage(string user, string message)
        {
            // var currentUser = await _userManager.GetUserAsync(System.Security.Claims.ClaimsPrincipal.Current);
            // user = currentUser.Email;
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
