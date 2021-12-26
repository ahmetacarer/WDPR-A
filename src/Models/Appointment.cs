using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace WDPR_A.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }

        [NotMapped]
        [Required]
        public Client? IncomingClient { get; set; }

        [NotMapped]
        public IList<Guardian>? Guardians { get; set; }

        [Required]
        public string? OrthopedagogueId { get; set; }
        
        [Required]
        public Orthopedagogue Orthopedagogue { get; set; }
    }
}