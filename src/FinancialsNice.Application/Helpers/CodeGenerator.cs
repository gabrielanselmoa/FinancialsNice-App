using System.Text;

namespace FinancialsNice.Application.Helpers;

public static class CodeGenerator
{
    private static readonly Random _random = new Random();

    public static string GenerateTransactionCode()
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string symbols = "#@";

        var sb = new StringBuilder();

        // Generates three random uppercase letters
        for (int i = 0; i < 3; i++)
        {
            sb.Append(letters[_random.Next(letters.Length)]);
        }

        // Adds a random symbol (# or @)
        sb.Append(symbols[_random.Next(symbols.Length)]);

        // Adds three random numbers
        for (int i = 0; i < 3; i++)
        {
            sb.Append(_random.Next(10)); // Numbers from 0 to 9
        }

        return sb.ToString();
    }

    public static string GenerateTicketCode()
    {
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string symbols = "!$";

        var sb = new StringBuilder();

        // Fixed prefix to identify as a ticket code
        sb.Append("TCK");

        // Adds two random letters
        for (int i = 0; i < 2; i++)
        {
            sb.Append(letters[_random.Next(letters.Length)]);
        }

        // Adds a random symbol (! or $)
        sb.Append(symbols[_random.Next(symbols.Length)]);

        // Adds four random numbers
        for (int i = 0; i < 4; i++)
        {
            sb.Append(_random.Next(10));
        }

        return sb.ToString();
    }
}