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
            if (state.Src[state.I] == '\\')
            {
                var end = state.I + 2;
                var nextChar = Utils.GetChar(state.Src, state.I + 1);
                if (nextChar == '\r')
                {
                    nextChar = Utils.GetChar(state.Src, state.I + 2);
                    end++;
                }
                if (nextChar == '\n')
                {
                    var hb = Utils.NewInline("hard_break", state.ParentIndex + state.I, state.Line, "\\", 0);
                    hb.Length = 2;
                    parent.Children!.Add(hb);
                    HandleHardBreakEnd(state, end);
                    return true;
                }
            }
            else if (state.Src[state.I] == ' ')
            {
                var end = state.Src.Length;
                var spaces = 1;
                for (var i = state.I + 1; i < state.Src.Length; i++)
                {
                    if (state.Src[i] == '\n')
                    {
                        end = i;
                        break;
                    }
                    else if (state.Src[i] == '\r')
                    {
                        // Keep going...
                    }
                    else if (state.Src[i] == ' ')
                    {
                        spaces++;
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (spaces >= 2)
                {
                    var hb = Utils.NewInline("hard_break", state.ParentIndex + state.I, state.Line, "  ", 0);
                    hb.Length = spaces;
                    parent.Children!.Add(hb);
                    HandleHardBreakEnd(state, end + 1);
                    return true;
                }
            }
        }

        return false;
    }

    private static void HandleHardBreakEnd(InlineParserState state, int end)
    {
        state.I = end;
        state.Line++;
        state.LineStart = state.I;

        // "Spaces at the end of the line and beginning of the next line are removed"
        if (state.I < state.Src.Length && Utils.IsSpace(state.Src[state.I]))
        {
            var space = Utils.ConsumeSpaces(state.Src, state.I);
            state.I += space.Length;
        }
    }
}
