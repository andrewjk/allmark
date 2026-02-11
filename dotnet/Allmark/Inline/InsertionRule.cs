namespace Allmark.Inline;

using Allmark.Types;

public static class InsertionRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "insertion",
			Test = TestInsertion,
		};
	}

	private static bool TestInsertion(InlineParserState state, MarkdownNode parent)
	{
		return CriticMarksRule.Execute("insertion", "+", state, parent);
	}
}
