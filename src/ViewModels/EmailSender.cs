using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailSender
{
    private static string? _ApiKey { get; set; } = "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"; 

    public static async Task SendEmail(string receiver, string subject, string body)
    {
        var client = new SendGridClient(_ApiKey);
        var from = new EmailAddress("zmdh.hhs@gmail.com", "ZMDH Kliniek");  //Voer verzender email in
        var to = new EmailAddress(receiver, "Intakegesprek cliënt");
        var plainTextContent = "";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, body);
        var response = await client.SendEmailAsync(msg);
    }

    public static async Task SetApiKey(string kvUri, string secretName)
    { 
        var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());
        var secret = await client.GetSecretAsync(secretName);
        _ApiKey = secret.Value.Value;
    }
}