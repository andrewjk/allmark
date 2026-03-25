namespace Allmark.Render;

using Allmark.Types;

public static class HighlightRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "highlight",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        RenderTag.Execute(node, state, "mark");
    }
}
