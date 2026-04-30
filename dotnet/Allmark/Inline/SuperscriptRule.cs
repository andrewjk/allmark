namespace Allmark.Inline;

using Allmark.Types;

public static class SuperscriptRule
{
    public static InlineRule Create()
    {
        return new InlineRule
        {
            Name = "superscript",
            Test = TestSuperscript,
            Precedence = 5,
        };
    }

    private static bool TestSuperscript(InlineParserState state, MarkdownNode parent)
    {
        var ch = Utils.GetChar(state.Src, state.I);
        if (!state.IsEscaped && ch == '^')
        {
            var rule = Create();
            return TagMarksRule.Execute("superscript", ch.ToString(), state, parent, rule.Precedence!.Value);
        }
        return false;
    }
}
