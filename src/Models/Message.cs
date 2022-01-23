using System.ComponentModel.DataAnnotations;

namespace WDPR_A.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public User Sender { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime When { get; set; }

        [Required]
        public string ChatRoomId { get; set; }

        [Required]
        public Chat Chat { get; set; }

        public int ReportCount { get; set; }
    }
}