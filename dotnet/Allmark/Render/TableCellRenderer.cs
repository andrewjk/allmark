namespace Allmark.Render;

using Allmark.Types;

public static class TableCellRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "table_cell",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        RenderTag.Execute(node, state, "td");
    }
}
