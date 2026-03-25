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
        if (state.I + 1 < state.Src.Length) {
            if (state.Src[state.I] == '\\' && Utils.IsNewLine(state.Src[state.I + 1]))
            {
                var hb = Utils.NewInline("hard_break", state.ParentIndex + state.I, state.Line, "\\", 0);
                hb.Length = 2;
                state.I += 2;
                parent.Children!.Add(hb);
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
                    var hb = Utils.NewInline("hard_break", state.ParentIndex + state.I, state.Line, "\\", 0);
                    hb.Length = end - state.I;
                    state.I = end + 1;
                    parent.Children!.Add(hb);
                    return true;
                }
            }
        }

        return false;
    }
}
