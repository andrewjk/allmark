namespace Allmark.Block;

using Allmark.Types;

public static class HeadingRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "heading",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		if (parent.AcceptsContent)
		{
			return false;
		}

		var ch = Utils.GetChar(state.Src, state.I);
		if (state.Indent <= 3 && ch == '#' && !Utils.IsEscaped(state.Src, state.I))
		{
			var level = 1;
			// TODO: peekUntil
			for (var j = state.I + 1; j < state.Src.Length; j++)
			{
				if (Utils.GetChar(state.Src, j) == '#')
				{
					level++;
				}
				else
				{
					break;
				}
			}

			if (level < 7 && Utils.IsSpace(Utils.GetChar(state.Src, state.I + level)))
			{
				MarkdownNode? closedNode = null;
				// TODO: consumeSpace(state, state.i + level)

				// If there's an open paragraph, close it
				// TODO: Is there a better way to do this??
				if (parent.Type == "paragraph")
				{
					closedNode = state.OpenNodes.Pop();
					parent = state.OpenNodes.Peek();
				}

				if (closedNode != null)
				{
					Utils.CloseNode(state, closedNode);
				}

				var heading = Utils.NewNode("heading", true, state.I, state.Line, 1, new string('#', level), 0, []);

				if (state.HasBlankLine && parent.Children!.Count > 0)
				{
					parent.Children[^1].BlankAfter = true;
					state.HasBlankLine = false;
				}

				parent.Children!.Add(heading);

				// HACK: ignore optional end heading marks and spaces, destructively
				state.I += level;
				var endOfLine = Utils.GetEndOfLine(state);
				var end = endOfLine - 1;
				for (; end >= state.I; end--)
				{
					if (!Utils.IsSpace(Utils.GetChar(state.Src, end)))
					{
						break;
					}
				}
				for (; end >= state.I; end--)
				{
					if (Utils.GetChar(state.Src, end) != '#')
					{
						if (Utils.GetChar(state.Src, end) == '\\' || !Utils.IsSpace(Utils.GetChar(state.Src, end)))
						{
							end = endOfLine - 1;
						}
						break;
					}
				}
				heading.Content = state.Src.Substring(state.I, end + 1 - state.I);
				state.I = endOfLine;

				return true;
			}
		}

		return false;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		return false;
	}
}
