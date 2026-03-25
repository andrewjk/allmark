namespace Allmark.Render;

using Allmark.Types;

public static class TableHeaderRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "table_header",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        RenderTag.Execute(node, state, "th");
    }
}
