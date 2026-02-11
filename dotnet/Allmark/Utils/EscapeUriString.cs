namespace Allmark;

public static partial class Utils
{
	public static string EscapeUriString(string text)
	{
		return Uri.EscapeUriString(text).Replace("[", "%5B").Replace("]", "%5D");
	}
}
