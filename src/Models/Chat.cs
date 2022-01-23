using System.ComponentModel.DataAnnotations;

namespace WDPR_A.Models;

public class Chat
{
    [Key]
    public string RoomId { get; set; } // dit is de primaire sleutel van de chatroom
    public string? Subject { get; set; }
    public string? Condition { get; set; }
    public bool IsPrivate { get; set; }
    public AgeCategory AgeCategory { get; set; }
    [Required]
    public Orthopedagogue Orthopedagogue { get; set; }
    public IList<Client> Clients { get; set; } = new List<Client>();

    public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
}