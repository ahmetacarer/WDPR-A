namespace src.Controllers
{
    public static class APIcall
    {
        private readonly static HttpClient client = new HttpClient();
        public static async Task GetClientFile(string birthDate, int bsn)
        {
            TimeZoneInfo gstZone = TimeZoneInfo.FindSystemTimeZoneById("Greenwich Standard Time");
            DateTime convertedTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gstZone);
            string dateTime = convertedTime.ToString("dd MM yyyy HH mm ss");
            // signature of date time in RSA SHA256 data 
            string encryptedData = SigningData.encryptData(dateTime);
            // send get request with the date time and the crypted data
            client.DefaultRequestHeaders.Add("key", $"{dateTime}|{encryptedData}");

            var response = await client.GetAsync("https://zorgdomeinhhs.azurewebsites.net/referral/" + birthDate + "/" + bsn);
            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(content);
        }

        // need to make it.
        // public static async Task GetClientInfo()
        // {
        //....code
        // }
    }
}