namespace Allmark;

using System.Text.RegularExpressions;

public static partial class Utils
{
	// TODO: Is this faster or slower than escapeBackslashes?
	public static string EscapePunctuation(string text)
	{
		return Regex.Replace(text, @"\\([!""#$%&'()*+,-./:;<=>?@[\\\]^_`{|}~])", "$1");
	}
}
