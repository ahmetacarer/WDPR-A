using System.ComponentModel.DataAnnotations;
namespace WDPR_A.Models
{
    public class Guardian : User
    {
        [Required]
        public IList<Client>? Clients { get; set; }
    }
}