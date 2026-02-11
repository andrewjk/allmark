namespace Allmark.Inline;

using Allmark.Types;

public static class TextRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "text",
			Test = TestText,
		};
	}

	private static bool TestText(InlineParserState state, MarkdownNode parent)
	{
		var ch = Utils.GetChar(state.Src, state.I);

		var lastNode = parent.Children!.LastOrDefault();
		if (lastNode == null || lastNode.Type != "text")
		{
			lastNode = Utils.NewNode("text", false, state.I, state.Line, 1, "", 0);
			parent.Children!.Add(lastNode);
		}
		else if (Utils.IsNewLine(ch))
		{
			// "Spaces at the end of the line and beginning of the next line are removed"
			lastNode.Markup = lastNode.Markup.TrimEnd();
		}

		if (Utils.IsAlphaNumeric(ch))
		{
			// If this an alphanumeric character, we can just process the whole
			// word, and save checking a bunch of characters that are never going to
			// match anything
			var start = state.I;
			state.I++;
			while (state.I < state.Src.Length && Utils.IsAlphaNumeric(state.Src[state.I]))
			{
				state.I++;
			}
			lastNode.Markup += state.Src.Substring(start, state.I - start);
		}
		else
		{
			state.I++;
			lastNode.Markup += ch;
		}

		return true;
	}
}
