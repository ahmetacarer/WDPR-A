using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace WDPR_A.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public Client? IncomingClient { get; set; }

        [Required]
        public string? IncomingClientId { get; set; }

        public bool IsVerified { get; set; }

        public IList<Guardian>? Guardians { get; set; }

        [Required]
        public string? OrthopedagogueId { get; set; }

        [Required]
        public Orthopedagogue Orthopedagogue { get; set; }
    }
}