namespace Allmark.Inline;

using Allmark.Types;

public static class HardBreakRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "hard_break",
			Test = TestHardBreak,
		};
	}

	private static bool TestHardBreak(InlineParserState state, MarkdownNode parent)
	{
		if (state.I + 1 < state.Src.Length && state.Src[state.I] == '\\' && Utils.IsNewLine(state.Src[state.I + 1]))
		{
			var hb = Utils.NewNode("hard_break", false, state.I, state.Line, 1, "\\", 0);
			state.I += 2;
			parent.Children!.Add(hb);
			return true;
		}

		return false;
	}
}
