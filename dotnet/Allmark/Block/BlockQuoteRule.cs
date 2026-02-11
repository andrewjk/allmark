namespace Allmark.Block;

using Allmark.Parse;
using Allmark.Types;

/// <summary>
/// A block quote is a sequence of lines that is considered to be a single unit.
/// </summary>
public static class BlockQuoteRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "block_quote",
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

			var quote = Utils.NewNode("block_quote", true, state.I, state.Line, 1, c.ToString(), quoteIndent, new List<MarkdownNode>());

			parent.Children!.Add(quote);
			state.OpenNodes.Push(quote);

			Utils.MovePastMarker(1, state);

			state.HasBlankLine = false;
			ParseBlock.Execute(state, quote);

			return true;
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
