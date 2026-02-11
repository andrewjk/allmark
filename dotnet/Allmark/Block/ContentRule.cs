namespace Allmark.Block;

using Allmark.Types;

public static class ContentRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "content",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		var endOfLine = Utils.GetEndOfLine(state);
		var content = state.Src.Substring(state.I, endOfLine - state.I);
		if (parent.AcceptsContent)
		{
			if (state.HasBlankLine)
			{
				//parent.content += "\n";
			}
			else
			{
				parent.Content += new string(' ', state.Indent);
			}
			parent.Content += content;
			state.HasBlankLine = false;
		}
		else
		{
			parent.Content += content;
		}
		state.I = endOfLine;
		return true;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		return false;
	}
}
