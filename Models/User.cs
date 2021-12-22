using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WDPR_A.Models

{
    public class User : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }

    }
}