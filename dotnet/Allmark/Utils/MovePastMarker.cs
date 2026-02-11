namespace Allmark;

using Allmark.Types;

public static partial class Utils
{
	public static void MovePastMarker(int markerLength, BlockParserState state)
	{
		// If the marker (e.g. '>' or '-') is followed by a tab, the markup is
		// considered to be '> ' followed by 2 spaces. Otherwise we reset the indent
		// for children
		state.I += markerLength;
		if (state.I < state.Src.Length) {
			if (state.Src[state.I] == '\t' && state.Src[state.I + 1] == '\t')
			{
				state.Indent = 6;
				state.I += 2;
			}
			else if (state.Src[state.I] == ' ')
			{
				state.Indent = 0;
				state.I += 1;
			}
		}
	}
}
