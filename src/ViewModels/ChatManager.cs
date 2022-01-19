using WDPR_A.Models;

namespace WDPR_A.ViewModels;
public class ChatManager
{
    private readonly Generate _generate;
    private readonly WDPRContext _context;


    public ChatManager(Generate generate, WDPRContext context)
    {
        _generate = generate;
        _context = context;
    }

    public async Task CreateSelfHelpChatAsync(Orthopedagogue orthopedagogue, string subject, string condition, AgeCategory ageCategory)
    {
        var chat = new Chat() { RoomId = Guid.NewGuid().ToString(), Subject = subject, Orthopedagogue = orthopedagogue, Condition = condition, AgeCategory = ageCategory, IsPrivate = false };
        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
    }

    public void DeleteChat(Chat chat)
    {
        _context.Chats.Remove(chat);
    }

    public void DeleteChat(string roomId)
    {
        if (!_context.Chats.Any(c => c.RoomId == roomId)) return;
        var chat = _context.Chats.SingleOrDefault(c => c.RoomId == roomId);
        _context.Chats.Remove(chat);
    }

    public async Task CreateChatAsync(Orthopedagogue orthopedagogue, Client client)
    {
        var chat = new Chat() { Orthopedagogue = orthopedagogue };
        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
    }
}