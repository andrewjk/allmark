namespace Allmark.Render;

using Allmark.Types;

public static class HtmlSpanRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "html_span",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        state.Output.Append(node.Content);
    }
}
