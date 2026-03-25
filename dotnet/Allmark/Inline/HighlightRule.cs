namespace Allmark.Inline;

using Allmark.Types;

public static class HighlightRule
{
    public static InlineRule Create()
    {
        return new InlineRule
        {
            Name = "highlight",
            Test = TestHighlight,
            Precedence = 5,
        };
    }

    private static bool TestHighlight(InlineParserState state, MarkdownNode parent)
    {
        var ch = Utils.GetChar(state.Src, state.I);
        if (ch == '=' && !Utils.IsEscaped(state.Src, state.I))
        {
            var rule = Create();
            return TagMarksRule.Execute("highlight", ch.ToString(), state, parent, rule.Precedence!.Value);
        }
        return false;
    }
}
