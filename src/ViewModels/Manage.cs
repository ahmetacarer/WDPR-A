using WDPR_A.Models;

namespace WDPR_A.ViewModels;
public class Manage
{
    private readonly Generate _generate;
    private readonly WDPRContext _context;


    public Manage(Generate generate, WDPRContext context)
    {
        _generate = generate;
        _context = context;
    }

    public async Task CreateChatAsync(Orthopedagogue orthopedagogue)
    {
        var chat = new Chat() { Code = GetUniqueChatCode(), Orthopedagogue = orthopedagogue};
        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
    }

    public async Task CreateChatAsync(Orthopedagogue orthopedagogue, Client client)
    {
        var code = GetUniqueChatCode();
        var chat = new Chat() { Code = code, Orthopedagogue = orthopedagogue};
        client.ChatCode = code;
        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
    }

    public string GetUniqueChatCode()
    {
        string chatCode = _generate.RandomChatCode();
        bool isUnique = !_context.Chats.Any(c => c.Code == chatCode);
        while (!isUnique)
        {
            chatCode = _generate.RandomChatCode();
            isUnique = !_context.Chats.Any(c => c.Code == chatCode);
        }
        return chatCode;
    }
}