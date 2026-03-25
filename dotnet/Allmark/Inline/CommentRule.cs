namespace Allmark.Inline;

using Allmark.Types;

public static class CommentRule
{
    public static bool Execute(InlineParserState state, MarkdownNode parent)
    {
        var rule = Create();
        return CriticMarksRule.Execute("comment", ">", state, parent, rule.Precedence!.Value, "<");
    }

    public static InlineRule Create()
    {
        return new InlineRule { Name = "comment", Test = Execute, Precedence = 20 };
    }
}
