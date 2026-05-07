namespace Allmark.Block;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class HeadingUnderlineRule
{
    public static BlockRule Create()
    {
        return new BlockRule
        {
            Name = "heading_underline",
            TestStart = TestStart,
            TestContinue = TestContinue,
        };
    }

    private static bool TestStart(BlockParserState state, MarkdownNode parent)
    {
        if (state.MaybeContinue)
        {
            for (var i = 0; i < state.OpenNodes.Count - 1; i++)
            {
                var node = state.OpenNodes.ElementAt(i);
                if (node.MaybeContinuing)
                {
                    return false;
                }
            }
        }

        var ch = Utils.GetChar(state.Src, state.I);
        if (state.Indent <= 3 && (ch == '=' || ch == '-'))
        {
            var matched = 1;
            var end = state.I + 1;
            for (; end < state.Src.Length; end++)
            {
                var nextChar = state.Src[end];
                if (nextChar == ch)
                {
                    // "The setext heading underline cannot contain internal spaces"
                    if (matched > 0 && Utils.IsSpace(state.Src[end - 1]))
                    {
                        return false;
                    }
                    matched++;
                }
                else if (Utils.IsNewLine(nextChar))
                {
                    end++;
                    break;
                }
                else if (Utils.IsSpace(nextChar))
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }

            // NOTE: We break from the spec here and require at least two underline
            // chars to prevent things from jumping around when typing a list under
            // a paragraph
            if (matched < 2)
            {
                return false;
            }

            var haveParagraph =
                parent.Type == "paragraph" && !parent.BlankAfter && Regex.IsMatch(parent.Content ?? "", @"[^\s]");
            if (haveParagraph)
            {
                parent.Type = "heading_underline";
                parent.Markup = state.Src.Substring(state.I, end - state.I);
                parent.Length = end - parent.Index;
                state.I = end;
                return true;
            }
        }

        return false;
    }

    private static bool TestContinue(BlockParserState state, MarkdownNode node)
    {
        return false;
    }
}
