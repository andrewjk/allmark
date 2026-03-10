namespace Allmark.Render;

using Allmark.Types;

public static class BlockQuoteRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "block_quote",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        RenderTag.Execute(node, state, "blockquote");
    }
}
