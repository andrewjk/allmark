namespace Allmark;

public static partial class Utils
{
	public static bool IsNewLine(char c)
	{
		return c == '\r' || c == '\n';
	}
}
