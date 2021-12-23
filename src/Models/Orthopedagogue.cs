using System.ComponentModel.DataAnnotations;
namespace WDPR_A.Models
{
    public class Orthopedagogue : User
    {
        [Required]
        public String? Specialty { get; set; }
        public IList<Appointment>? Appointments { get; set; }
    }
}