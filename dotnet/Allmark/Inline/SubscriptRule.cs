namespace Allmark.Inline;

using Allmark.Types;

public static class SubscriptRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "subscript",
			Test = TestSubscript,
		};
	}

	private static bool TestSubscript(InlineParserState state, MarkdownNode parent)
	{
		var ch = Utils.GetChar(state.Src, state.I);
		if (ch == '~' && !Utils.IsEscaped(state.Src, state.I))
		{
			// Subscripts can only be one character long, otherwise they are a GFM strikethrough
			if (Utils.GetChar(state.Src, state.I + 1) == '~')
			{
				return false;
			}
			return TagMarksRule.Execute("subscript", ch.ToString(), state, parent);
		}
		return false;
	}
}
