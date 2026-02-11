namespace Allmark;

public static partial class Utils
{
	public static char GetChar(string text, int i)
	{
		return i < text.Length ? text[i] : default;
	}
}
