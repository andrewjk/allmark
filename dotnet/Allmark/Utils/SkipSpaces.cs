namespace Allmark;

public static partial class Utils
{
	public static int SkipSpaces(string text, int start)
	{
		for (; start < text.Length; start++)
		{
			if (!IsSpace((int)text[start]))
			{
				break;
			}
		}
		return start;
	}
}
