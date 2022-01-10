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
        var chat = new Chat() { PrivateChatToken = GetUniquePrivateChatToken(), Orthopedagogue = orthopedagogue};
        await _context.Chats.AddAsync(chat);
        await _context.SaveChangesAsync();
    }

    public async Task CreateChatAsync(Orthopedagogue orthopedagogue, Client client)
    {
        var privateChatToken = GetUniquePrivateChatToken();
        var chat = new Chat() { PrivateChatToken = privateChatToken, Orthopedagogue = orthopedagogue};
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