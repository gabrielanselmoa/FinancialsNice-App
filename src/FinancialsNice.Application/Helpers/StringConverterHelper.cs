namespace FinancialsNice.Application.Helpers;

public static class StringConverterHelper
{
    public static string ToString(string[] array)
    {
        return string.Join(",", array);
    }
    
    public static string[] ToArray(string str)
    {
        return !string.IsNullOrEmpty(str) ? str.Split(',') : [];
    }
}
