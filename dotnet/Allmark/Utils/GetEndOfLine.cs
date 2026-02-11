namespace Allmark;

using Allmark.Types;

public static partial class Utils
{
	// TODO: This should be consumeUntil
	public static int GetEndOfLine(BlockParserState state)
	{
		int endOfLine = state.I;
		for (; endOfLine < state.Src.Length; endOfLine++)
		{
			if (IsNewLine(state.Src[endOfLine]))
			{
				endOfLine++;
				state.LineStart = endOfLine;
				break;
			}
		}
		return endOfLine;
	}
}
