namespace Allmark.Inline;

using Allmark.Types;

public static class DeletionRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "deletion",
			Test = TestDeletion,
		};
	}

	private static bool TestDeletion(InlineParserState state, MarkdownNode parent)
	{
		return CriticMarksRule.Execute("deletion", "-", state, parent);
	}
}
