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
        var allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*".ToCharArray();
        string randomWord = "";
        for (int i = 0; i < length; i++)
        {
            int randomIndex = _random.Next(allowedCharacters.Length);
            randomWord += allowedCharacters[randomIndex];
        }
        return randomWord;
    }

    
}