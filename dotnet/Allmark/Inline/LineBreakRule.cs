namespace Allmark.Inline;

using Allmark.Types;

public static class LineBreakRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "line_break",
			Test = TestLineBreak,
		};
	}

	private static bool TestLineBreak(InlineParserState state, MarkdownNode parent)
	{
		var ch = Utils.GetChar(state.Src, state.I);
		if (ch == ' ')
		{
			var end = state.I;
			for (var i = state.I + 1; i < state.Src.Length; i++)
			{
				if (Utils.IsNewLine(state.Src[i]))
				{
					end = i;
					break;
				}
				else if (state.Src[i] == ' ')
				{
					continue;
				}
				else
				{
					return false;
				}
			}
			if (end - state.I >= 2)
			{
				var html = Utils.NewNode("html_span", false, state.I, state.Line, 1, "", state.Indent);
				html.Content += "<br />\n";
				parent.Children!.Add(html);
				state.I = end + 1;
				return true;
			}
		}

		return false;
	}
}
