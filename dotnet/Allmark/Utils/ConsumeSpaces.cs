namespace Allmark;

public static partial class Utils
{
	public static string ConsumeSpaces(string text, int i)
	{
		var result = "";
		for (; i < text.Length; i++)
		{
			char c = text[i];
			if (IsSpace((int)c))
			{
				result += c;
			}
			else
			{
				break;
			}
		}
		return result;
	}
}
