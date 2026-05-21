namespace Allmark.Block;

using Allmark.Types;

public static class ListBulletedRule
{
    public static BlockRule Create()
    {
        return new BlockRule
        {
            Name = "list_bulleted",
            TestStart = TestStart,
            TestContinue = TestContinue,
            CloseNode = CloseNode,
        };
    }

    private static ListInfo? GetMarkup(BlockParserState state)
    {
        var ch = Utils.GetChar(state.Src, state.I);
        if (
            (ch == '-' || ch == '+' || ch == '*') &&
            (state.I == state.Src.Length - 1 || Utils.IsSpace(state.Src[state.I + 1])))
        {
            var delimiter = Utils.GetChar(state.Src, state.I).ToString();
            var nextChar = Utils.GetChar(state.Src, state.I + 1);
            if (nextChar == '\r')
            {
                nextChar = Utils.GetChar(state.Src, state.I + 2);
            }
            return new ListInfo
            {
                Delimiter = delimiter,
                Markup = delimiter,
                IsBlank = state.I == state.Src.Length - 1 || nextChar == '\n',
                Type = "list_bulleted"
            };
        }
        return null;
    }

    private static bool TestStart(BlockParserState state, MarkdownNode parent)
    {
        if (parent.AcceptsContent)
        {
            return false;
        }

        var info = GetMarkup(state);
        return ListRule.TestListStart(state, parent, info);
    }

    private static bool TestContinue(BlockParserState state, MarkdownNode node)
    {
        var info = GetMarkup(state);
        return ListRule.TestListContinue(state, node, info);
    }

    private static void CloseNode(BlockParserState state, MarkdownNode node)
    {
        node.Loose = ListRule.IsLooseList(node);
    }
}
