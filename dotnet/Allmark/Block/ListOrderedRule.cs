namespace Allmark.Block;

using Allmark.Types;

public static class ListOrderedRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "list_ordered",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	private static ListInfo? GetMarkup(BlockParserState state)
	{
		var numbers = "";
		var end = state.I;
		while (end < state.Src.Length && Utils.IsNumeric(state.Src[end]))
		{
			numbers += Utils.GetChar(state.Src, end);
			end++;
		}
		var orderedList =
			numbers.Length > 0 &&
			numbers.Length < 10 &&
			(state.Src[end] == '.' || state.Src[end] == ')') &&
			(end == state.Src.Length - 1 || Utils.IsSpace(state.Src[end + 1]));
		if (orderedList)
		{
			var delimiter = Utils.GetChar(state.Src, end).ToString();
			return new ListInfo
			{
				Delimiter = delimiter,
				Markup = numbers + delimiter,
				IsBlank = end == state.Src.Length - 1 || Utils.IsNewLine(state.Src[end + 1]),
				Type = "list_ordered"
			};
		}
		return null;
	}

	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		if (parent.AcceptsContent)
		{
			return false;
		}

		var info = GetMarkup(state);
		return ListRule.TestListStart(state, parent, info);
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		var info = GetMarkup(state);
		return ListRule.TestListContinue(state, node, info);
	}
}
