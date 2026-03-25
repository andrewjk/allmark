namespace Allmark.Inline;

using Allmark.Types;

public static class StrikethroughRule
{
    public static InlineRule Create()
    {
        return new InlineRule
        {
            Name = "strikethrough",
            Test = TestStrikethrough,
            Precedence = 5,
        };
    }

    private static bool TestStrikethrough(InlineParserState state, MarkdownNode parent)
    {
        var ch = Utils.GetChar(state.Src, state.I);
        if (ch == '~' && !Utils.IsEscaped(state.Src, state.I))
        {
            var rule = Create();
            return TagMarksRule.Execute("strikethrough", ch.ToString(), state, parent, rule.Precedence!.Value);
        }
        return false;
    }
}
