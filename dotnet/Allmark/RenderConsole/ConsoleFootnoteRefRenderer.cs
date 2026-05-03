namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleFootnoteRefRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "footnote_ref",
            Render = (node, state, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        if (node.Info != null)
        {
            state.FootnoteRefs[node.Info] = node;
        }
    }
}
