using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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

        public string GetConnectionId() => Context.ConnectionId;

        public Task JoinRoom(string roomId) => Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        
        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        
        public async Task RemoveFromGroupIfBlocked(string userId, string roomId)
        {
            var client = await _context.Clients.FindAsync(userId);
            if (client == null || !client.IsBlocked) return;
            await RemoveFromGroup(roomId);
        }


        public async Task SendMessage(string text, string roomId)
        {
            var currentUser = await _userManager.GetUserAsync(Context.User);
            var contextUser = _context.InheritedUsers.SingleOrDefault(u => u.Id == currentUser.Id);
            await RemoveFromGroupIfBlocked(contextUser.Id, roomId);
            var name = $"{contextUser.FirstName[0]}. {contextUser.LastName}";
            var date = DateTime.Now;
            await Clients.Groups(roomId).SendAsync("ReceiveMessage", name, text, date.ToString("HH:mm"));
            await _context.Messages.AddAsync(new Message { Sender = contextUser, Text = text, When = date, ChatRoomId = roomId });
            await _context.SaveChangesAsync();
        }
    }
}
