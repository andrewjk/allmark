namespace Allmark.Block;

using Allmark.Types;

public static class ListBulletedRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "list_bulleted",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	private static ListInfo? GetMarkup(BlockParserState state)
	{
		var ch = Utils.GetChar(state.Src, state.I);
		if (
			(ch == '-' || ch == '+' || ch == '*') &&
			// TODO: Should this be part of the isSpace/isNewLine check? i.e. eof counts as a space?
			(state.I == state.Src.Length - 1 || Utils.IsSpace(state.Src[state.I + 1])))
		{
			return new ListInfo
			{
				Delimiter = ch.ToString(),
				Markup = ch.ToString(),
				IsBlank = state.I == state.Src.Length - 1 || Utils.IsNewLine(state.Src[state.I + 1]),
				Type = "list_bulleted"
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
