using System.Text;

namespace Sork;

public static class StringExtensions
{
    public static string NetworkCleanup(this string input)
    {
        var sb = new StringBuilder();
        foreach (var c in input)
        {
            if (c == '\b' || c == '\u007F')
            {
                if (sb.Length > 0)
                {
                    sb.Length--;
                }
            }
            else
            {
                sb.Append(c);
            }
        }
        return sb.ToString();
    }

    public static string Capitalize(this string s)
    {
        return s.Substring(0, 1).ToUpper() + s.Substring(1);
    }
}