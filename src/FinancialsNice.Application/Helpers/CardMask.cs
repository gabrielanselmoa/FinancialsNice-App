namespace FinancialsNice.Application.Helpers;

public static class CardMask
{
    public static string MaskCardNumber(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return string.Empty;
        
        var digits = new string(raw.Where(char.IsDigit).ToArray());

        if (digits.Length < 8)
        {
            var first = digits.Length >= 4 ? digits[..4] : digits;
            var rest = digits.Length > 4 ? digits[4..] : string.Empty;
            return $"{first} {new string('*', rest.Length)}";
        }

        var first4 = digits[..4];
        var last4 = digits[^4..];
        var masked = "•••• ••••";

        return $"{first4} {masked} {last4}";
    }
}