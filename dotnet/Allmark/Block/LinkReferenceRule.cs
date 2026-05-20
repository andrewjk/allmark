namespace Allmark.Block;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class LinkReferenceRule
{
    public static BlockRule Create()
    {
        return new BlockRule
        {
            Name = "link_ref",
            TestStart = TestStart,
            TestContinue = (_, _) => false,
        };
    }

    private static bool TestStart(BlockParserState state, MarkdownNode parent)
    {
        if (parent.AcceptsContent)
        {
            return false;
        }

        var ch = Utils.GetChar(state.Src, state.I);
        if (!state.IsEscaped && state.Indent <= 3 && ch == '[')
        {
            // "A link reference definition cannot interrupt a paragraph"
            if (parent.Type == "paragraph" && !parent.BlankAfter)
            {
                return false;
            }

            var originalIndex = state.I;
            var start = state.I + 1;

            // Get the label
            var label = "";
            for (var i = start; i < state.Src.Length; i++)
            {
                if (!Utils.IsEscaped(state.Src, i))
                {
                    if (state.Src[i] == ']')
                    {
                        label = state.Src.Substring(start, i - start);
                        start = i + 1;
                        break;
                    }

                    // "Link labels cannot contain brackets, unless they are
                    // backslash-escaped"
                    if (state.Src[i] == '[')
                    {
                        return false;
                    }
                }
            }
            // "A link label must contain at least one non-whitespace character"
            if (string.IsNullOrEmpty(label) || !Regex.IsMatch(label, @"[^\s]"))
            {
                return false;
            }

            if (start > state.Src.Length - 1 || state.Src[start] != ':')
            {
                return false;
            }

            start++;

            var linkInfo = Utils.ParseLinkBlock(state, start, "\n");
            if (linkInfo == null)
            {
                return false;
            }

            // "As noted in the section on Links, matching of labels is
            // case-insensitive (see matches)"
            label = Utils.NormalizeLabel(label);

            // "If there are several matching definitions, the first one takes
            // precedence"
            if (state.Refs.ContainsKey(label))
            {
                return true;
            }

            state.Refs[label] = linkInfo;

            var refNode = Utils.NewBlock("link_ref", originalIndex, state.Line, "", 0);

            if (state.HasBlankLine && parent.Children!.Count > 0)
            {
                parent.Children[^1].BlankAfter = true;
                state.HasBlankLine = false;
            }

            parent.Children!.Add(refNode);

            if (!Utils.IsNewLine(Utils.GetChar(state.Src, state.I - 1)))
            {
                state.I = Utils.GetEndOfLine(state);
            }

            refNode.Length = state.I - refNode.Index;

            return true;
        }

        return false;
    }
}
