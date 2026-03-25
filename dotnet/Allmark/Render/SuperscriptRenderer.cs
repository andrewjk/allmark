namespace Allmark.Render;

using Allmark.Types;

public static class SuperscriptRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "superscript",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        RenderTag.Execute(node, state, "sup");
    }
}
