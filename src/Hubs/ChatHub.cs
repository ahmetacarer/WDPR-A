using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WDPR_A.Models;

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

        public async Task SendMessage(string text, string roomId)
        {
            var currentUser = await _userManager.GetUserAsync(Context.User);
            var contextUser = _context.InheritedUsers.SingleOrDefault(u => u.Id == currentUser.Id);
            var name = $"{contextUser.FirstName[0]}. {contextUser.LastName}";
            var date = DateTime.Now;
            // niet de meest efficiente manier, later gerefactored worden naar meerdere methods zoals SendMessageToChat en SendPrivateMessage
            await Clients.All.SendAsync("ReceiveMessage", name, text, date.ToString("HH:mm"));
            await _context.Messages.AddAsync(new Message { Sender = contextUser, Text = text, When = date, ChatRoomId = roomId });
            await _context.SaveChangesAsync();
        }
    }
}
