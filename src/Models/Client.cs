using System.ComponentModel.DataAnnotations;
namespace WDPR_A.Models
{
    public class Client : User
    {
        [Required]
        public String? Condition { get; set; }  //Aandoening
        private AgeCategory AgeCategory { get; set; }
        public string? ChatCode { get; set; }  //Eenmalige code
        public IList<Guardian>? Guardians { get; set; }

        public AgeCategory DecideAgeCategory(int age)
        {
            if(age < 12) 
            {
                return AgeCategory = AgeCategory.Jongste;
            }
            else if (age >= 12 && age < 16)
            {
                return AgeCategory = AgeCategory.Middelste;
            }
            return AgeCategory.Oudste;
        }
    }
}