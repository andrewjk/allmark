namespace Allmark;

using Allmark.Types;

public static partial class Utils
{
    public static void CloseNode(BlockParserState state, MarkdownNode node)
    {
        node.Length = state.I - node.Index;
        if (state.RulesMap.TryGetValue(node.Type, out var rule))
        {
            rule.CloseNode?.Invoke(state, node);
        }
    }
}
