using System.ComponentModel.DataAnnotations;

namespace WDPR_A.Models;

public class Chat
{
    [Key]
    public string Code { get; set; }
    [Required]
    public Orthopedagogue Orthopedagogue { get; set; }
    public IList<Client> Clients { get; set; }
}