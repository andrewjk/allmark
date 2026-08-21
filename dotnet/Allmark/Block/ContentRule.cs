namespace Allmark.Block;

using Allmark.Types;

public static class ContentRule
{
    public static BlockRule Create()
    {
        return new BlockRule
        {
            Name = "content",
            TestStart = TestStart,
            TestContinue = (_, _) => false,
        };
    }

    private static bool TestStart(BlockParserState state, MarkdownNode parent, int endOfLine)
    {
        var content = state.Src.Substring(state.I, endOfLine - state.I) + Utils.GetLineEnding(state, endOfLine);
        if (parent.AcceptsContent)
        {
            if (state.HasBlankLine)
            {
                state.HasBlankLine = false;
            }
            else
            {
                parent.Content += new string(' ', state.Indent);
            }
        }
        else
        {
            parent.Content += state.Spaces;
            state.Spaces = "";
        }
        parent.Content += content;
        return true;
    }
}
