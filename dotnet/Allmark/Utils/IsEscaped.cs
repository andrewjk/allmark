namespace Allmark;

public static partial class Utils
{
	public static bool IsEscaped(string text, int i)
	{
		return i > 0 && text[i - 1] == '\\' && (i <= 1 || text[i - 2] != '\\');
	}
}
