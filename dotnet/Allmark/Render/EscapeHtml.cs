namespace Allmark.Render;

public static class EscapeHtml
{
	public static string Execute(string text)
	{
		return text.Replace("&", "&");
	}
}
