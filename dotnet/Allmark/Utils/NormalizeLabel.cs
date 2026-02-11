namespace Allmark;

using System.Text.RegularExpressions;

/// <summary>
/// "To normalize a label, strip off the opening and closing brackets, perform
/// the Unicode case fold, strip leading and trailing whitespace and collapse
/// consecutive internal whitespace to a single space."
/// </summary>
public static partial class Utils
{
	public static string NormalizeLabel(string text)
	{
		return Regex.Replace(text.ToLower().ToUpper().Trim(), @"\s+", " ");
	}
}
