namespace Allmark.Block;

using Allmark.Types;

public static class IndentRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "indent",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	// TODO: Should this be built in and not a rule??
	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		if (state.I < state.Src.Length && Utils.IsSpace(state.Src[state.I]))
		{
			for (; state.I < state.Src.Length; state.I++)
			{
				var ch = Utils.GetChar(state.Src, state.I);
				if (ch == ' ')
				{
					// TODO: All the other spaces
					state.Indent += 1;
				}
				else if (ch == '\t')
				{
					// Set spaces to the next tabstop of 4 characters (e.g. for '  \t', set
					// the spaces to 4)
					state.Indent += 4 - (state.Indent % 4);
				}
				else
				{
					break;
				}
			}
		}

		return false;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		return false;
	}
}
