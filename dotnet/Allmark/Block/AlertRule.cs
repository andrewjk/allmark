namespace Allmark.Block;

using System.Text.RegularExpressions;
using Allmark.Types;

/// <summary>
/// Alerts, also sometimes known as callouts or admonitions, are a Markdown
/// extension based on the blockquote syntax.
/// </summary>
public static class AlertRule
{
	private static readonly Regex AlertRegex = new(@"^\s*\[!(note|tip|important|warning|caution)]", RegexOptions.IgnoreCase);

	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "alert",
			TestStart = TestStart,
			TestContinue = TestContinue,
			CloseNode = Close
		};
	}

	private static bool HasMarkup(char c, BlockParserState state)
	{
		return state.Indent <= 3 && c == '>';
	}

	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		MarkdownNode? closedNode = null;

		if (parent.AcceptsContent)
		{
			return false;
		}

		char c = Utils.GetChar(state.Src, state.I);
		if (HasMarkup(c, state))
		{
			var match = AlertRegex.Match(state.Src[(state.I + 1)..]);
			if (match.Success)
			{
				if (parent.Type == "paragraph")
				{
					closedNode = state.OpenNodes.Pop();
					parent = state.OpenNodes.Peek();
				}

				if (closedNode != null)
				{
					Utils.CloseNode(state, closedNode);
				}

				int quoteIndent = state.Indent + 1;

				var quote = Utils.NewNode("alert", true, state.I, state.Line, 1, match.Groups[1].Value.ToLowerInvariant(), quoteIndent, new List<MarkdownNode>());

				parent.Children!.Add(quote);
				state.OpenNodes.Push(quote);

				state.I = Utils.GetEndOfLine(state);

				return true;
			}
		}

		return false;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		char c = Utils.GetChar(state.Src, state.I);
		if (HasMarkup(c, state))
		{
			Utils.MovePastMarker(1, state);
			return true;
		}

		if (state.HasBlankLine)
		{
			return false;
		}

		var openNode = state.OpenNodes.Peek();
		if (openNode.Type == "paragraph")
		{
			state.MaybeContinue = true;
			node.MaybeContinuing = true;
			return true;
		}

		return false;
	}

	private static void Close(BlockParserState state, MarkdownNode node)
	{
		if (state.HasBlankLine && node.Children != null && node.Children.Count > 0)
		{
			node.Children[^1].BlankAfter = true;
			state.HasBlankLine = false;
		}
	}
}
