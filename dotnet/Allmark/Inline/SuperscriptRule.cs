namespace Allmark.Inline;

using Allmark.Types;

public static class SuperscriptRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "superscript",
			Test = TestSuperscript,
		};
	}

	private static bool TestSuperscript(InlineParserState state, MarkdownNode parent)
	{
		var ch = Utils.GetChar(state.Src, state.I);
		if (ch == '^' && !Utils.IsEscaped(state.Src, state.I))
		{
			return TagMarksRule.Execute("superscript", ch.ToString(), state, parent);
		}
		return false;
	}
}
