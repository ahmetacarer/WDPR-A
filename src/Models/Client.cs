using System.ComponentModel.DataAnnotations;
namespace WDPR_A.Models
{
    public class Client : User
    {
        [Required]
        public String? Condition { get; set; }  //Aandoening

        [Required]
        public AgeCategory AgeCategory { get; set; }

        [Required]
        public IList<Guardian>? Guardians { get; set; }

        [Required]
        public IList<Chat>? Chats { get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        public string Woonplaats { get; set; }


        public AgeCategory DecideAgeCategory(int age)
        {
            if (age < 12)
            {
                return AgeCategory = AgeCategory.Jongste;
            }
            else if (age >= 12 && age < 16)
            {
                return AgeCategory = AgeCategory.Middelste;
            }
            return AgeCategory = AgeCategory.Oudste;
        }
    }
}