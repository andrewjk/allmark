namespace Allmark.Render;

using Allmark.Types;

public static class EmphasisRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "emphasis",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        RenderTag.Execute(node, state, "em");
    }
}
