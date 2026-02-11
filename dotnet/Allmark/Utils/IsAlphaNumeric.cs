namespace Allmark;

public static partial class Utils
{
	public static bool IsAlpha(int code)
	{
		return (code > 64 && code < 91) || (code > 96 && code < 123);
	}

	public static bool IsNumeric(int code)
	{
		return code > 47 && code < 58;
	}

	public static bool IsAlphaNumeric(int code)
	{
		return IsAlpha(code) || IsNumeric(code);
	}
}
