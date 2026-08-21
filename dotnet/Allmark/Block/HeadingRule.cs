namespace Allmark.Block;

using Allmark.Types;

public static class HeadingRule
{
    public static BlockRule Create()
    {
        return new BlockRule
        {
            Name = "heading",
            TestStart = TestStart,
            TestContinue = (_, _) => false,
        };
    }

    private static bool TestStart(BlockParserState state, MarkdownNode parent, int endOfLine)
    {
        if (parent.AcceptsContent)
        {
            return false;
        }

        var ch = Utils.GetChar(state.Src, state.I);
        if (!state.IsEscaped && state.Indent <= 3 && ch == '#')
        {
            var level = 1;
            // TODO: peekUntil
            for (var j = state.I + 1; j < state.Src.Length; j++)
            {
                if (Utils.GetChar(state.Src, j) == '#')
                {
                    level++;
                }
                else
                {
                    break;
                }
            }

            if (level < 7 && Utils.IsSpace(Utils.GetChar(state.Src, state.I + level)))
            {
                MarkdownNode? closedNode = null;
                // TODO: consumeSpace(state, state.i + level)

                // If there's an open paragraph, close it
                // TODO: Is there a better way to do this??
                if (parent.Type == "paragraph")
                {
                    closedNode = state.OpenNodes.Pop();
                    parent = state.OpenNodes.Peek();
                }

                if (closedNode != null)
                {
                    Utils.CloseNode(state, closedNode);
                }

                var heading = Utils.NewBlock("heading", state.I, state.Line, new string('#', level), 0);

                if (state.HasBlankLine && parent.Children!.Count > 0)
                {
                    parent.Children[^1].BlankAfter = true;
                    state.HasBlankLine = false;
                }

                parent.Children!.Add(heading);

                Utils.MovePastMarker(level, state);
                var end = endOfLine - 1;
                for (; end >= state.I; end--)
                {
                    if (!Utils.IsSpace(Utils.GetChar(state.Src, end)))
                    {
                        break;
                    }
                }
                for (; end >= state.I; end--)
                {
                    if (Utils.GetChar(state.Src, end) != '#')
                    {
                        if (Utils.GetChar(state.Src, end) == '\\' || !Utils.IsSpace(Utils.GetChar(state.Src, end)))
                        {
                            end = endOfLine - 1;
                        }
                        break;
                    }
                }
                end++;

                var content = Utils.NewBlock("heading_content", state.I, state.Line, "", 0);
                content.Content = state.Src.Substring(state.I, end - state.I);
                heading.Children = [content];

                if (end < endOfLine)
                {
                    heading.Info = state.Src.Substring(end, endOfLine - end);
                }

                heading.Length = endOfLine - heading.Index;
                content.Length = endOfLine - content.Index;

                return true;
            }
        }

        return false;
    }
}
