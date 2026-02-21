namespace Allmark.Render;

using Allmark.Types;

public static class CommentRenderer
{
    public static Renderer Create()
    {
        return new Renderer { Name = "comment", Render = Execute };
    }

    public static void Execute(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        RenderUtils.StartNewLine(node, state);
        state.Output.Append("<span class=\"markdown-comment\">");
        RenderChildren.Execute(node, state, decode);
        state.Output.Append("</span>");
        RenderUtils.EndNewLine(node, state);
    }
}
