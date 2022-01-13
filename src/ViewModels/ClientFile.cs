using System.Text.Json.Serialization;

namespace WDPR_A.ViewModels
{
    public class ClientFile
    {
        [JsonPropertyName("naw")]
        public PersonDetails PersonDetails { get; set;}
        [JsonPropertyName("redenVanVerwijzing")]
        public string ReasonForReferral { get; set; }
        [JsonPropertyName("verzekering")]
        public string insurance { get; set; }
    }

    public class PersonDetails
    {
        [JsonPropertyName("bsn")]
        public string BSN { get; set; }
        [JsonPropertyName("naam")]
        public string FullName { get; set; }
        [JsonPropertyName("adres")]
        public string Address { get; set; }
        [JsonPropertyName("woonplaats")]
        public string Residence { get; set; }
        [JsonPropertyName("postcode")]
        public string ZipCode { get; set; }
        [JsonPropertyName("geboorteDatum")]
        public string BirthDate { get; set; }
    }
}