using Microsoft.AspNetCore.Authorization;
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

        public async Task<bool> IsBlocked(string userId, string roomId)
        {
            var client = await _context.Clients.FindAsync(userId);
            var chat = await _context.Chats.SingleOrDefaultAsync(c => c.RoomId == roomId);
            if (client == null || !client.IsBlocked || chat.IsPrivate)
                return false;
            return true;
        }
        //
        public async Task SendMessage(string text, string roomId)
        {
            var currentUser = await _userManager.GetUserAsync(Context.User);
            var contextUser = await _context.InheritedUsers.SingleOrDefaultAsync(u => u.Id == currentUser.Id);
            var blockedStatus = await IsBlocked(contextUser.Id, roomId);

            var name = $"{contextUser.FirstName[0]}. {contextUser.LastName}";
            var date = DateTime.Now;
            await Clients.Group(roomId).SendAsync("ReceiveMessage", name, text, date.ToString("HH:mm"));
            if (!blockedStatus)
            {

                await _context.Messages.AddAsync(new Message { Sender = contextUser, Text = text, When = date, ChatRoomId = roomId });
                await _context.SaveChangesAsync();
            }
        }
    }
}
