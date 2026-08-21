namespace Allmark;

using System.Text.RegularExpressions;

public static partial class Utils
{
    // TODO: Is this faster or slower than escapeBackslashes?
    public static string EscapePunctuation(string text)
    {
        // Fast path: no backslashes means nothing to unescape
        if (text.IndexOf('\\') < 0)
        {
            return text;
        }
        return Regex.Replace(text, @"\\([!""#$%&'()*+,-./:;<=>?@[\\\]^_`{|}~])", "$1");
    }
}
