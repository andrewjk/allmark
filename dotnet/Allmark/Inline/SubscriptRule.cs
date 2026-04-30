namespace Allmark.Inline;

using Allmark.Types;

public static class SubscriptRule
{
    public static InlineRule Create()
    {
        return new InlineRule
        {
            Name = "subscript",
            Test = TestSubscript,
            Precedence = 5,
        };
    }

    private static bool TestSubscript(InlineParserState state, MarkdownNode parent)
    {
        var ch = Utils.GetChar(state.Src, state.I);
        if (!state.IsEscaped && ch == '~')
        {
            // Subscripts can only be one character long, otherwise they are a GFM strikethrough
            if (Utils.GetChar(state.Src, state.I + 1) == '~')
            {
                return false;
            }
            var rule = Create();
            return TagMarksRule.Execute("subscript", ch.ToString(), state, parent, rule.Precedence!.Value);
        }
        return false;
    }
}
