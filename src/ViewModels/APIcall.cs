namespace src.Controllers
{
    public static class APIcall
    {
        private readonly static HttpClient client = new HttpClient();
        public static async Task<String> GetClientFile(string birthDate, int bsn)
        {
            TimeZoneInfo gstZone = TimeZoneInfo.FindSystemTimeZoneById("Greenwich Standard Time");
            DateTime convertedTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, gstZone);
            string dateTime = convertedTime.ToString("dd MM yyyy HH mm ss");

            // signature of date time in RSA SHA256 data 
            string encryptedData = await SigningData.EncryptData(dateTime);

            // send get request with the date time and the crypted data
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("key", $"{dateTime}|{encryptedData}");
            
            HttpResponseMessage response = await client.GetAsync("https://zorgdomeinhhs.azurewebsites.net/referral/" + birthDate + "/" + bsn);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "Error";
            }
        }

        // need to make it.
        // public static async Task GetClientInfo()
        // {
        //....code
        // }
    }
}