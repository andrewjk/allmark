namespace Allmark.Inline;

using Allmark.Types;

public static class CommentRule
{
    public static bool Execute(InlineParserState state, MarkdownNode parent)
    {
        return CriticMarksRule.Execute("comment", ">", state, parent, "<");
    }

    public static InlineRule Create()
    {
        return new InlineRule { Name = "comment", Test = Execute };
    }
}
