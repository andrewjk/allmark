namespace Allmark.Parse;

using Allmark.Types;

public static class ParseIndent
{
    public static void Execute(BlockParserState state)
    {
        if (state.I < state.Src.Length && Utils.IsSpace(state.Src[state.I]))
        {
            for (; state.I < state.Src.Length; state.I++)
            {
                char c = Utils.GetChar(state.Src, state.I);
                if (c == ' ')
                {
                    state.Indent += 1;
                }
                else if (c == '\t')
                {
                    state.Indent += 4 - (state.Indent % 4);
                }
                else if (c == '\n')
                {
                    state.HasBlankLine = true;
                    break;
                }
                else if (c == '\r')
                {
                    if (Utils.GetChar(state.Src, state.I + 1) != '\n')
                    {
                        // Only break for a CR on its own, otherwise the LF will get caught next
                        state.HasBlankLine = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
