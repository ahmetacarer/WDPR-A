namespace WDPR_A.ViewModels;
public class Generate
{
    private readonly Random _random;

    public Generate(Random random) // dependency injection through constructor
    {
        _random = random;
    }

    public string RandomString(int length)
    {
        if (length < 0) throw new Exception("number must be bigger or equal to 0");
        var allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*".ToCharArray();
        string randomWord = "";
        for (int i = 0; i < length; i++)
        {
            int randomIndex = _random.Next(allowedCharacters.Length);
            randomWord += allowedCharacters[randomIndex];
        }
        return randomWord;
    }

    public string RandomChatCode()
    {
        int fixedLength = 8;
        return RandomString(fixedLength);
    }
}