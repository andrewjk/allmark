namespace Allmark.Render;

using Allmark.Types;

public static class ParagraphRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "paragraph",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        RenderUtils.StartNewLine(node, state);
        state.Output.Append("<p>");
        RenderUtils.InnerNewLine(node, state);
        RenderChildren.Execute(node, state);
        state.Output.Append("</p>");
        RenderUtils.EndNewLine(node, state);
    }
}
