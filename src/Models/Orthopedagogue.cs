using System.ComponentModel.DataAnnotations;
namespace WDPR_A.Models
{
    public class Orthopedagogue : User
    {
        [Required]
        public string? Specialty { get; set; }
        public IList<Appointment>? Appointments { get; set; }
        public IList<Chat>? Chats { get; set; }
    }
}