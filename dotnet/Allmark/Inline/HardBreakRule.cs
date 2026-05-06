namespace Allmark.Inline;

using Allmark.Types;

public static class HardBreakRule
{
    public static InlineRule Create()
    {
        return new InlineRule
        {
            Name = "hard_break",
            Test = TestHardBreak,
        };
    }

    private static bool TestHardBreak(InlineParserState state, MarkdownNode parent)
    {
        if (state.I + 1 < state.Src.Length)
        {
            if (state.Src[state.I] == '\\' && Utils.IsNewLine(state.Src[state.I + 1]))
            {
                var bump = 2;
                if (
                    Utils.GetChar(state.Src, state.I + 1) == '\r' &&
                    Utils.GetChar(state.Src, state.I + 2) == '\n'
                )
                {
                    bump++;
                }
                var hb = Utils.NewInline("hard_break", state.ParentIndex + state.I, state.Line, "\\", 0);
                hb.Length = 2;
                parent.Children!.Add(hb);
                state.I += bump;
                return true;
            }
            else if (state.Src[state.I] == ' ')
            {
                var end = state.I;
                for (var i = state.I + 1; i < state.Src.Length; i++)
                {
                    if (Utils.IsNewLine(state.Src[i]))
                    {
                        end = i;
                        break;
                    }
                    else if (state.Src[i] == ' ')
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (end - state.I >= 2)
                {
                    var bump = 1;
                    if (
                        Utils.GetChar(state.Src, end) == '\r' &&
                        Utils.GetChar(state.Src, end + 1) == '\n'
                    )
                    {
                        bump++;
                    }
                    var hb = Utils.NewInline("hard_break", state.ParentIndex + state.I, state.Line, "\\", 0);
                    hb.Length = end - state.I;
                    parent.Children!.Add(hb);
                    state.I = end + bump;
                    return true;
                }
            }
        }

        return false;
    }
}
