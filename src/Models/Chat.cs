using System.ComponentModel.DataAnnotations;

namespace WDPR_A.Models;

public class Chat
{
    [Key]
    public string Id { get; set; } // dit is de primaire sleutel van de chatroom
    public string ChatCode { get; set; } // dit is de unieke code van de client om aan de chat deel te nemen
    [Required]
    public Orthopedagogue Orthopedagogue { get; set; }
    public IList<Client> Clients { get; set; }

    public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
}