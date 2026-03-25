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
            Precedence = 20,
        };
    }

    private static bool TestDeletion(InlineParserState state, MarkdownNode parent)
    {
        var rule = Create();
        return CriticMarksRule.Execute("deletion", "-", state, parent, rule.Precedence!.Value);
    }
}
