namespace Allmark.Inline;

using Allmark.Types;

public static class TextRule
{
    public static InlineRule Create()
    {
        return new InlineRule
        {
            Name = "text",
            Test = TestText,
        };
    }

    private static bool TestText(InlineParserState state, MarkdownNode parent)
    {
        var ch = Utils.GetChar(state.Src, state.I);

        var lastNode = parent.Children!.LastOrDefault();
        if (lastNode == null || lastNode.Type != "text")
        {
            lastNode = Utils.NewText(state.ParentIndex + state.I, state.Line, "", 0);
            parent.Children!.Add(lastNode);
        }
        else if (Utils.IsNewLine(ch))
        {
            // "Spaces at the end of the line and beginning of the next line are removed"
            lastNode.Content = lastNode.Content.TrimEnd();
            if (state.I + 1 < state.Src.Length && Utils.IsSpace(Utils.GetChar(state.Src, state.I + 1)))
            {
                lastNode.Content += ch;
                lastNode.Length = lastNode.Content.Length;
                state.I += 2;
                while (state.I < state.Src.Length && Utils.IsSpace(state.Src[state.I]))
                {
                    state.I++;
                }
                lastNode = Utils.NewText(state.ParentIndex + state.I, state.Line, "", 0);
                parent.Children!.Add(lastNode);
                if (state.I < state.Src.Length)
                {
                    ch = Utils.GetChar(state.Src, state.I);
                }
            }
        }

        if (Utils.IsAlphaNumeric(ch))
        {
            // If this an alphanumeric character, we can just process the whole
            // word, and save checking a bunch of characters that are never going to
            // match anything
            var start = state.I;
            state.I++;
            while (state.I < state.Src.Length && Utils.IsAlphaNumeric(state.Src[state.I]))
            {
                state.I++;
            }
            lastNode.Content += state.Src.Substring(start, state.I - start);
        }
        else
        {
            state.I++;
            lastNode.Content += ch;
        }

        lastNode.Length = lastNode.Content.Length;

        return true;
    }
}
