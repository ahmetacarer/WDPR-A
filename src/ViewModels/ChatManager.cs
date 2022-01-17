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

    public async Task CreateSelfHelpChatAsync(Orthopedagogue orthopedagogue, string roomName, string subject, AgeCategory ageCategory)
    {
        var chat = new Chat() { RoomId = Guid.NewGuid().ToString(), RoomName = roomName, Orthopedagogue = orthopedagogue, Subject = subject, AgeCategory = ageCategory, IsPrivate = false };
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
        var privateChatToken = GetUniquePrivateChatToken();
        var chat = new Chat() { PrivateChatToken = privateChatToken, Orthopedagogue = orthopedagogue };
        client.PrivateChatToken = privateChatToken;
        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
    }

    public string GetUniquePrivateChatToken()
    {
        string PrivateChatToken = _generate.RandomPrivateChatToken();
        bool isUnique = !_context.Chats.Any(c => c.PrivateChatToken == PrivateChatToken);
        while (!isUnique)
        {
            PrivateChatToken = _generate.RandomPrivateChatToken();
            isUnique = !_context.Chats.Any(c => c.PrivateChatToken == PrivateChatToken);
        }
        return PrivateChatToken;
    }
}