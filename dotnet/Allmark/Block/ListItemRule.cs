namespace Allmark.Block;

using Allmark.Types;

public static class ListItemRule
{
    public static BlockRule Create()
    {
        return new BlockRule
        {
            Name = "list_item",
            TestStart = TestStart,
            TestContinue = TestContinue,
        };
    }

    private static bool TestStart(BlockParserState state, MarkdownNode parent)
    {
        return false;
    }

    private static bool TestContinue(BlockParserState state, MarkdownNode node)
    {
        var ch = Utils.GetChar(state.Src, state.I);

        if (state.Indent >= node.Subindent)
        {
            state.Indent -= node.Subindent;
            return true;
        }

        // This only applies to the lowest list_item
        MarkdownNode? itemNode = null;
        for (var i = 0; i < state.OpenNodes.Count - 1; i++)
        {
            var openNode = state.OpenNodes.ElementAt(i);
            if (openNode.Type == "list_item")
            {
                itemNode = openNode;
            }
            else if (openNode.Type == "list_ordered")
            {
                var numbers = "";
                var end = state.I;
                while (end < state.Src.Length && Utils.IsNumeric(state.Src[end]))
                {
                    numbers += Utils.GetChar(state.Src, end);
                    end++;
                }
                var delimiter = Utils.GetChar(state.Src, end);
                if (
                    state.Indent <= 3 &&
                    state.Indent < itemNode!.Subindent &&
                    numbers.Length > 0 &&
                    delimiter.ToString() == node.Delimiter)
                {
                    return false;
                }
                // Break only when content is inside this nesting level, otherwise
                // continue walking up to check ancestor lists
                if (state.Indent >= itemNode!.Subindent)
                {
                    break;
                }
            }
            else if (openNode.Type == "list_bulleted")
            {
                if (state.Indent <= 3 && state.Indent < itemNode!.Subindent && ch.ToString() == node.Delimiter)
                {
                    return false;
                }
                // Break only when content is inside this nesting level, otherwise
                // continue walking up to check ancestor lists
                if (state.Indent >= itemNode!.Subindent)
                {
                    break;
                }
            }
        }

        if (state.HasBlankLine)
        {
            return true;
        }

        var peekNode = state.OpenNodes.Peek();
        if (peekNode.Type == "paragraph")
        {
            // We won't know until we try more things
            state.MaybeContinue = true;
            node.MaybeContinuing = true;
            return true;
        }

        return false;
    }
}
