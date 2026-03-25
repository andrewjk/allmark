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
            Precedence = 20,
        };
    }

    private static bool TestInsertion(InlineParserState state, MarkdownNode parent)
    {
        var rule = Create();
        return CriticMarksRule.Execute("insertion", "+", state, parent, rule.Precedence!.Value);
    }
}
