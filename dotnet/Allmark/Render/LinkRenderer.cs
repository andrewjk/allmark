namespace Allmark.Render;

using Allmark.Types;

public static class LinkRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "link",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        RenderUtils.StartNewLine(node, state);
        var title = !string.IsNullOrEmpty(node.Title) ? $" title=\"{node.Title}\"" : "";
        state.Output.Append($"<a href=\"{Utils.EscapeHtml(node.Info ?? "")}\"{title}>");
        RenderChildren.Execute(node, state);
        state.Output.Append("</a>");
        RenderUtils.EndNewLine(node, state);
    }
}
