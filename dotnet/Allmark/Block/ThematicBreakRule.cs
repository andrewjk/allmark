namespace Allmark.Block;

using Allmark.Types;

public static class ThematicBreakRule
{
    public static BlockRule Create()
    {
        return new BlockRule
        {
            Name = "thematic_break",
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
        if (state.Indent <= 3 && (ch == '-' || ch == '_' || ch == '*'))
        {
            var matched = 1;
            var end = state.I + 1;
            for (; end < state.Src.Length; end++)
            {
                var nextChar = state.Src[end];
                if (nextChar == ch)
                {
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

            if (matched >= 3)
            {
                MarkdownNode? closedNode = null;

                if (state.MaybeContinue)
                {
                    state.MaybeContinue = false;
                    for (var i = 0; i < state.OpenNodes.Count - 1; i++)
                    {
                        var node = state.OpenNodes.ElementAt(i);
                        if (node.MaybeContinuing)
                        {
                            node.MaybeContinuing = false;
                            closedNode = node;
                            // Pop until we reach this node
                            var newLength = state.OpenNodes.Count - i - 1;
                            while (state.OpenNodes.Count > newLength)
                            {
                                state.OpenNodes.Pop();
                            }
                            break;
                        }
                    }
                    parent = state.OpenNodes.Peek();
                }

                if (parent.Type == "paragraph")
                {
                    closedNode = state.OpenNodes.Pop();
                    parent = state.OpenNodes.Peek();
                }

                if (parent.Type == "list_bulleted" || parent.Type == "list_ordered")
                {
                    state.OpenNodes.Pop();
                    parent = state.OpenNodes.Peek();
                }

                if (closedNode != null)
                {
                    Utils.CloseNode(state, closedNode);
                }

                var markup = state.Src.Substring(state.I, end - state.I);
                var tbr = Utils.NewBlock("thematic_break", state.I, state.Line, markup, 0);
                tbr.Length = end - state.I;
                parent.Children!.Add(tbr);
                state.I = end;
                return true;
            }
        }

        return false;
    }
}
