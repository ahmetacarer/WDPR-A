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

    public void CreateChat(Orthopedagogue orthopedagogue)
    {
        Chat chat = new Chat() { Code = UniqueChatCode(), Orthopedagogue = orthopedagogue};
    }

    public string UniqueChatCode()
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