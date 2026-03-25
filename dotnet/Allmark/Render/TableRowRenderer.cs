namespace Allmark.Render;

using Allmark.Types;

public static class TableRowRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "table_row",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        RenderTag.Execute(node, state, "tr");
    }
}
