namespace Allmark;

public static partial class Utils
{
	public static string EscapeBackslashes(string text)
	{
		var result = "";
		for (int i = 0; i < text.Length; i++)
		{
			char c = text[i];
			if (c == '\\' && IsPunctuation((int)text[i + 1]))
			{
				i++;
				c = text[i];
			}
			result += c;
		}
		return result;
	}
}
