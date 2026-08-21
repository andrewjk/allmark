namespace Allmark;

using System.Text.RegularExpressions;

public static partial class Utils
{
    private static readonly Dictionary<string, string> HtmlEscapes = new()
    {
        ["&"] = "&amp;",
        ["<"] = "&lt;",
        [">"] = "&gt;",
        ["\""] = "&quot;",
    };

    public static string EscapeHtml(string text)
    {
        // Fast path: nothing to escape, return the original string
        if (text.AsSpan().IndexOfAny("&<>\"") < 0)
        {
            return text;
        }
        return Regex.Replace(text, "[&<>\"]", match => HtmlEscapes[match.Value]);
    }
}
