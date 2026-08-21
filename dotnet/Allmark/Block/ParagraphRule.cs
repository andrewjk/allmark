namespace Allmark.Block;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class ParagraphRule
{
    public static BlockRule Create()
    {
        return new BlockRule
        {
            Name = "paragraph",
            TestStart = TestStart,
            TestContinue = TestContinue,
        };
    }

    private static bool TestStart(BlockParserState state, MarkdownNode parent, int endOfLine)
    {
        if (parent.AcceptsContent)
        {
            return false;
        }

        if (parent.Type == "paragraph" && !parent.BlankAfter)
        {
            return false;
        }

        var content = state.Src.Substring(state.I, endOfLine - state.I) + Utils.GetLineEnding(state, endOfLine);

        if (!Regex.IsMatch(content, @"[^\s]"))
        {
            return true;
        }

        var paragraph = Utils.NewBlock("paragraph", state.I, state.Line, "", 0);
        paragraph.Content = content;

        if (state.HasBlankLine && parent.Children!.Count > 0)
        {
            parent.Children[^1].BlankAfter = true;
            state.HasBlankLine = false;
        }

        parent.Children!.Add(paragraph);
        state.OpenNodes.Push(paragraph);

        return true;
    }

    private static bool TestContinue(BlockParserState state, MarkdownNode node)
    {
        if (state.HasBlankLine)
        {
            return false;
        }

        return true;
    }
}
